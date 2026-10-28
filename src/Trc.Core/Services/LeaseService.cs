using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Укладання, продовження та розірвання договорів оренди.</summary>
public class LeaseService : ServiceBase
{
    public LeaseService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    public List<Lease> GetAll(LeaseStatus? status = null, string? search = null)
    {
        using var db = ContextFactory();
        var query = db.Leases.AsNoTracking().Include(l => l.Tenant).Include(l => l.Premises).AsQueryable();
        if (status.HasValue)
            query = query.Where(l => l.Status == status.Value);

        var list = query.OrderByDescending(l => l.StartDate).ThenBy(l => l.Number).ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            list = list.Where(l => ContainsText(l.Number, search) || ContainsText(l.Tenant.CompanyName, search)
                                   || ContainsText(l.Premises.Number, search)).ToList();
        }
        return list;
    }

    public Lease Get(int id)
    {
        using var db = ContextFactory();
        return db.Leases.AsNoTracking().Include(l => l.Tenant).Include(l => l.Premises).FirstOrDefault(l => l.Id == id)
               ?? throw new BusinessException("Договір не знайдено.");
    }

    /// <summary>Наступний вільний номер договору у форматі «Д-РРРР-NNN».</summary>
    public string GenerateNumber(int year)
    {
        using var db = ContextFactory();
        return GenerateNumber(db, year);
    }

    /// <summary>
    /// Укладає договір оренди. Якщо орендну плату не вказано, вона обчислюється як площа × ставка.
    /// </summary>
    public Lease Create(int tenantId, int premisesId, DateTime startDate, DateTime endDate,
                        decimal? monthlyRent = null, decimal deposit = 0)
    {
        startDate = startDate.Date;
        endDate = endDate.Date;
        if (endDate <= startDate)
            throw new BusinessException("Дата закінчення договору має бути пізнішою за дату початку.");
        if (endDate < startDate.AddMonths(1).AddDays(-1))
            throw new BusinessException("Мінімальний строк оренди — один місяць.");
        if (endDate < Clock.Today)
            throw new BusinessException("Неможливо укласти договір, строк дії якого вже минув.");
        Guard.NotNegative(deposit, "Депозит");

        using var db = ContextFactory();
        var tenant = db.Tenants.Find(tenantId) ?? throw new BusinessException("Орендаря не знайдено.");
        var premises = db.Premises.Find(premisesId) ?? throw new BusinessException("Приміщення не знайдено.");

        if (premises.Status == PremisesStatus.Maintenance)
            throw new BusinessException($"Приміщення {premises.Number} перебуває на ремонті.");
        EnsureNoOverlap(db, premisesId, startDate, endDate, excludeLeaseId: null);

        var rent = monthlyRent ?? premises.BaseMonthlyRent;
        Guard.Positive(rent, "Орендна плата");

        var lease = new Lease
        {
            Number = GenerateNumber(db, startDate.Year),
            Tenant = tenant,
            Premises = premises,
            StartDate = startDate,
            EndDate = endDate,
            MonthlyRent = Math.Round(rent, 2),
            Deposit = Math.Round(deposit, 2),
            Status = LeaseStatus.Active
        };
        premises.Status = PremisesStatus.Occupied;

        db.Leases.Add(lease);
        db.SaveChanges();
        return lease;
    }

    /// <summary>Продовжує строк дії договору.</summary>
    public void Extend(int leaseId, DateTime newEndDate)
    {
        newEndDate = newEndDate.Date;
        using var db = ContextFactory();
        var lease = db.Leases.Find(leaseId) ?? throw new BusinessException("Договір не знайдено.");
        if (lease.Status != LeaseStatus.Active)
            throw new BusinessException("Продовжити можна лише діючий договір.");
        if (newEndDate <= lease.EndDate)
            throw new BusinessException("Нова дата закінчення має бути пізнішою за поточну.");

        EnsureNoOverlap(db, lease.PremisesId, lease.StartDate, newEndDate, excludeLeaseId: lease.Id);
        lease.EndDate = newEndDate;
        db.SaveChanges();
    }

    /// <summary>
    /// Достроково розриває договір: приміщення звільняється,
    /// а неоплачені нарахування за наступні місяці анулюються.
    /// </summary>
    public void Terminate(int leaseId, DateTime terminationDate)
    {
        terminationDate = terminationDate.Date;
        using var db = ContextFactory();
        var lease = db.Leases.Include(l => l.Premises).Include(l => l.Payments).FirstOrDefault(l => l.Id == leaseId)
                    ?? throw new BusinessException("Договір не знайдено.");
        if (lease.Status != LeaseStatus.Active)
            throw new BusinessException("Розірвати можна лише діючий договір.");
        if (terminationDate < lease.StartDate)
            throw new BusinessException("Дата розірвання не може передувати даті початку договору.");

        lease.Status = LeaseStatus.Terminated;
        if (terminationDate < lease.EndDate)
            lease.EndDate = terminationDate;

        var lastPeriod = new DateTime(terminationDate.Year, terminationDate.Month, 1);
        var futureCharges = lease.Payments
            .Where(p => p.Status == PaymentStatus.Pending && new DateTime(p.PeriodYear, p.PeriodMonth, 1) > lastPeriod)
            .ToList();
        db.Payments.RemoveRange(futureCharges);

        lease.Premises.Status = PremisesStatus.Free;
        db.SaveChanges();
    }

    /// <summary>
    /// Переводить договори, строк яких минув, у стан «Завершений» і звільняє приміщення.
    /// Повертає кількість оновлених договорів.
    /// </summary>
    public int CloseExpired()
    {
        using var db = ContextFactory();
        var today = Clock.Today;
        var expired = db.Leases.Include(l => l.Premises)
            .Where(l => l.Status == LeaseStatus.Active && l.EndDate < today)
            .ToList();

        foreach (var lease in expired)
        {
            lease.Status = LeaseStatus.Expired;
            lease.Premises.Status = PremisesStatus.Free;
        }
        db.SaveChanges();
        return expired.Count;
    }

    private static void EnsureNoOverlap(TrcDbContext db, int premisesId, DateTime start, DateTime end, int? excludeLeaseId)
    {
        var conflict = db.Leases.Include(l => l.Tenant)
            .Where(l => l.PremisesId == premisesId && l.Status == LeaseStatus.Active && l.Id != excludeLeaseId)
            .FirstOrDefault(l => l.StartDate <= end && l.EndDate >= start);

        if (conflict is not null)
            throw new BusinessException(
                $"Приміщення вже орендоване за договором {conflict.Number} ({conflict.Tenant.CompanyName}) " +
                $"до {conflict.EndDate:dd.MM.yyyy}.");
    }

    private static string GenerateNumber(TrcDbContext db, int year)
    {
        var prefix = $"Д-{year}-";
        var numbers = db.Leases.Where(l => l.Number.StartsWith(prefix)).Select(l => l.Number).ToList();
        var max = numbers.Select(n => int.TryParse(n[prefix.Length..], out var x) ? x : 0).DefaultIfEmpty(0).Max();
        return $"{prefix}{max + 1:000}";
    }
}
