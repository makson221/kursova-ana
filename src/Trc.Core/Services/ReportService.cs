using System.ComponentModel;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Основні показники роботи ТРЦ для головної панелі.</summary>
public sealed record DashboardStats(
    int PremisesTotal,
    int PremisesOccupied,
    decimal TotalArea,
    decimal OccupiedArea,
    int ActiveTenants,
    int ActiveLeases,
    int LeasesExpiringSoon,
    decimal MonthlyRentIncome,
    decimal TotalDebt,
    int DebtorsCount,
    int Employees,
    int ParkingOccupied,
    int ParkingCapacity,
    int UpcomingEvents)
{
    public double OccupancyPercent => TotalArea == 0 ? 0 : (double)Math.Round(OccupiedArea / TotalArea * 100, 1);
}

public sealed record MonthlyRevenue(
    [property: Browsable(false)] int Month,
    [property: DisplayName("Місяць")] string MonthName,
    [property: DisplayName("Оренда, грн")] decimal Rent,
    [property: DisplayName("Квитки, грн")] decimal Tickets,
    [property: DisplayName("Паркінг, грн")] decimal Parking,
    [property: DisplayName("Разом, грн")] decimal Total);

public sealed record FloorOccupancy(
    [property: DisplayName("Поверх")] int Floor,
    [property: DisplayName("Приміщень")] int PremisesCount,
    [property: DisplayName("Орендовано")] int OccupiedCount,
    [property: DisplayName("Площа, м²")] decimal Area,
    [property: DisplayName("Орендована площа, м²")] decimal OccupiedArea,
    [property: DisplayName("Заповненість, %")] double OccupancyPercent);

public sealed record CategoryStat(
    [property: DisplayName("Категорія")] string Category,
    [property: DisplayName("Орендарів")] int Tenants,
    [property: DisplayName("Орендована площа, м²")] decimal Area,
    [property: DisplayName("Оренда на місяць, грн")] decimal MonthlyRent);

/// <summary>Аналітичні звіти.</summary>
public class ReportService : ServiceBase
{
    private readonly ParkingSettings _parkingSettings;

    public ReportService(Func<TrcDbContext> contextFactory, IClock clock, ParkingSettings? parkingSettings = null)
        : base(contextFactory, clock)
    {
        _parkingSettings = parkingSettings ?? new ParkingSettings();
    }

    public DashboardStats GetDashboard()
    {
        using var db = ContextFactory();
        var now = Clock.Now;
        var today = now.Date;
        var soon = today.AddDays(30);

        var premises = db.Premises.AsNoTracking().ToList();
        var activeLeases = db.Leases.AsNoTracking().Where(l => l.Status == LeaseStatus.Active).ToList();
        var overdue = db.Payments.AsNoTracking()
            .Where(p => p.Status == PaymentStatus.Pending && p.DueDate < today)
            .Select(p => new { p.Amount, p.Lease.TenantId })
            .ToList();

        return new DashboardStats(
            PremisesTotal: premises.Count,
            PremisesOccupied: premises.Count(p => p.Status == PremisesStatus.Occupied),
            TotalArea: premises.Sum(p => p.Area),
            OccupiedArea: premises.Where(p => p.Status == PremisesStatus.Occupied).Sum(p => p.Area),
            ActiveTenants: activeLeases.Select(l => l.TenantId).Distinct().Count(),
            ActiveLeases: activeLeases.Count,
            LeasesExpiringSoon: activeLeases.Count(l => l.EndDate <= soon),
            MonthlyRentIncome: activeLeases.Sum(l => l.MonthlyRent),
            TotalDebt: overdue.Sum(p => p.Amount),
            DebtorsCount: overdue.Select(p => p.TenantId).Distinct().Count(),
            Employees: db.Employees.Count(e => e.IsActive),
            ParkingOccupied: db.ParkingSessions.Count(s => s.ExitTime == null),
            ParkingCapacity: _parkingSettings.Capacity,
            UpcomingEvents: db.Events.Count(e => e.Status == EventStatus.Planned && e.StartTime >= now));
    }

    /// <summary>Фактичні надходження за місяцями року (оренда за датою оплати, квитки, паркінг).</summary>
    public List<MonthlyRevenue> GetRevenueByMonth(int year)
    {
        using var db = ContextFactory();
        var from = new DateTime(year, 1, 1);
        var to = from.AddYears(1);

        var rent = db.Payments.AsNoTracking()
            .Where(p => p.Status == PaymentStatus.Paid && p.PaidDate >= from && p.PaidDate < to)
            .Select(p => new { p.PaidDate, p.Amount }).ToList()
            .GroupBy(p => p.PaidDate!.Value.Month).ToDictionary(g => g.Key, g => g.Sum(p => p.Amount));

        var tickets = db.Tickets.AsNoTracking()
            .Where(t => t.SoldAt >= from && t.SoldAt < to)
            .Select(t => new { t.SoldAt, t.TotalPrice }).ToList()
            .GroupBy(t => t.SoldAt.Month).ToDictionary(g => g.Key, g => g.Sum(t => t.TotalPrice));

        var parking = db.ParkingSessions.AsNoTracking()
            .Where(s => s.ExitTime >= from && s.ExitTime < to)
            .Select(s => new { s.ExitTime, s.Fee }).ToList()
            .GroupBy(s => s.ExitTime!.Value.Month).ToDictionary(g => g.Key, g => g.Sum(s => s.Fee ?? 0));

        var culture = CultureInfo.GetCultureInfo("uk-UA");
        return Enumerable.Range(1, 12).Select(m =>
        {
            var r = rent.GetValueOrDefault(m);
            var t = tickets.GetValueOrDefault(m);
            var p = parking.GetValueOrDefault(m);
            var name = culture.TextInfo.ToTitleCase(culture.DateTimeFormat.GetMonthName(m));
            return new MonthlyRevenue(m, name, r, t, p, r + t + p);
        }).ToList();
    }

    public List<FloorOccupancy> GetOccupancyByFloor()
    {
        using var db = ContextFactory();
        return db.Premises.AsNoTracking().ToList()
            .GroupBy(p => p.Floor)
            .OrderBy(g => g.Key)
            .Select(g =>
            {
                var area = g.Sum(p => p.Area);
                var occupied = g.Where(p => p.Status == PremisesStatus.Occupied).ToList();
                var occupiedArea = occupied.Sum(p => p.Area);
                var percent = area == 0 ? 0 : (double)Math.Round(occupiedArea / area * 100, 1);
                return new FloorOccupancy(g.Key, g.Count(), occupied.Count, area, occupiedArea, percent);
            })
            .ToList();
    }

    public List<CategoryStat> GetTenantsByCategory()
    {
        using var db = ContextFactory();
        return db.Leases.AsNoTracking()
            .Where(l => l.Status == LeaseStatus.Active)
            .Include(l => l.Tenant).Include(l => l.Premises)
            .ToList()
            .GroupBy(l => l.Tenant.Category)
            .Select(g => new CategoryStat(g.Key.GetDisplayName(), g.Select(l => l.TenantId).Distinct().Count(),
                                          g.Sum(l => l.Premises.Area), g.Sum(l => l.MonthlyRent)))
            .OrderByDescending(c => c.MonthlyRent)
            .ToList();
    }
}
