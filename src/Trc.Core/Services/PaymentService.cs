using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Інформація про заборгованість орендаря.</summary>
public sealed record DebtorInfo(int TenantId, string TenantName, string Phone, int UnpaidCount, decimal Debt, DateTime OldestDueDate);

/// <summary>Нарахування орендної плати та облік оплат.</summary>
public class PaymentService : ServiceBase
{
    /// <summary>День місяця, до якого має бути сплачена оренда.</summary>
    public const int DueDay = 10;

    public PaymentService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    public List<Payment> GetAll(PaymentFilter filter = PaymentFilter.All, int? year = null, int? month = null, string? search = null)
    {
        using var db = ContextFactory();
        var today = Clock.Today;
        var query = db.Payments.AsNoTracking()
            .Include(p => p.Lease).ThenInclude(l => l.Tenant)
            .Include(p => p.Lease).ThenInclude(l => l.Premises)
            .AsQueryable();

        if (year.HasValue) query = query.Where(p => p.PeriodYear == year.Value);
        if (month.HasValue) query = query.Where(p => p.PeriodMonth == month.Value);

        query = filter switch
        {
            PaymentFilter.Unpaid => query.Where(p => p.Status == PaymentStatus.Pending),
            PaymentFilter.Overdue => query.Where(p => p.Status == PaymentStatus.Pending && p.DueDate < today),
            PaymentFilter.Paid => query.Where(p => p.Status == PaymentStatus.Paid),
            _ => query
        };

        var list = query.OrderByDescending(p => p.PeriodYear).ThenByDescending(p => p.PeriodMonth)
            .ThenBy(p => p.Lease.Number).ToList();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            list = list.Where(p => ContainsText(p.Lease.Tenant.CompanyName, search) || ContainsText(p.Lease.Number, search)
                                   || ContainsText(p.Lease.Premises.Number, search)).ToList();
        }
        return list;
    }

    /// <summary>
    /// Формує нарахування за місяць для всіх діючих договорів, що охоплюють цей місяць.
    /// Повторний виклик не створює дублікатів. Повертає кількість нових нарахувань.
    /// </summary>
    public int GenerateCharges(int year, int month)
    {
        if (month is < 1 or > 12)
            throw new BusinessException("Невірний номер місяця.");

        var periodStart = new DateTime(year, month, 1);
        var periodEnd = periodStart.AddMonths(1).AddDays(-1);

        using var db = ContextFactory();
        var leases = db.Leases
            .Where(l => l.Status == LeaseStatus.Active && l.StartDate <= periodEnd && l.EndDate >= periodStart)
            .Where(l => !l.Payments.Any(p => p.PeriodYear == year && p.PeriodMonth == month))
            .ToList();

        foreach (var lease in leases)
        {
            db.Payments.Add(new Payment
            {
                LeaseId = lease.Id,
                PeriodYear = year,
                PeriodMonth = month,
                Amount = lease.MonthlyRent,
                DueDate = periodStart.AddDays(DueDay - 1),
                Status = PaymentStatus.Pending
            });
        }
        db.SaveChanges();
        return leases.Count;
    }

    /// <summary>Реєструє оплату нарахування.</summary>
    public void MarkPaid(int paymentId, DateTime? paidDate = null)
    {
        using var db = ContextFactory();
        var payment = db.Payments.Find(paymentId) ?? throw new BusinessException("Платіж не знайдено.");
        if (payment.Status == PaymentStatus.Paid)
            throw new BusinessException("Цей платіж уже сплачено.");

        var date = (paidDate ?? Clock.Now).Date;
        if (date > Clock.Today)
            throw new BusinessException("Дата оплати не може бути в майбутньому.");

        payment.Status = PaymentStatus.Paid;
        payment.PaidDate = date;
        db.SaveChanges();
    }

    /// <summary>Скасовує помилково зареєстровану оплату.</summary>
    public void CancelPayment(int paymentId)
    {
        using var db = ContextFactory();
        var payment = db.Payments.Find(paymentId) ?? throw new BusinessException("Платіж не знайдено.");
        if (payment.Status != PaymentStatus.Paid)
            throw new BusinessException("Платіж ще не сплачено.");

        payment.Status = PaymentStatus.Pending;
        payment.PaidDate = null;
        db.SaveChanges();
    }

    /// <summary>Перелік орендарів, що мають прострочені платежі.</summary>
    public List<DebtorInfo> GetDebtors()
    {
        using var db = ContextFactory();
        var today = Clock.Today;
        var overdue = db.Payments.AsNoTracking()
            .Include(p => p.Lease).ThenInclude(l => l.Tenant)
            .Where(p => p.Status == PaymentStatus.Pending && p.DueDate < today)
            .ToList();

        return overdue
            .GroupBy(p => p.Lease.TenantId)
            .Select(g =>
            {
                var tenant = g.First().Lease.Tenant;
                return new DebtorInfo(tenant.Id, tenant.CompanyName, tenant.Phone, g.Count(),
                                      g.Sum(p => p.Amount), g.Min(p => p.DueDate));
            })
            .OrderByDescending(d => d.Debt)
            .ToList();
    }

    public decimal GetTotalDebt() => GetDebtors().Sum(d => d.Debt);
}
