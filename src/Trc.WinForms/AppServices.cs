using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;
using Trc.Core.Services;

namespace Trc.WinForms;

/// <summary>
/// Контейнер сервісів бізнес-логіки, спільний для всіх форм,
/// а також інформація про поточного користувача.
/// </summary>
public sealed class AppServices
{
    public AppServices(Func<TrcDbContext> contextFactory, IClock clock, string databasePath)
    {
        Clock = clock;
        DatabasePath = databasePath;
        var parking = new ParkingSettings();

        Auth = new AuthService(contextFactory, clock);
        Premises = new PremisesService(contextFactory, clock);
        Tenants = new TenantService(contextFactory, clock);
        Leases = new LeaseService(contextFactory, clock);
        Payments = new PaymentService(contextFactory, clock);
        Employees = new EmployeeService(contextFactory, clock);
        Events = new EventService(contextFactory, clock);
        Parking = new ParkingService(contextFactory, clock, parking);
        Reports = new ReportService(contextFactory, clock, parking);
    }

    public IClock Clock { get; }
    public string DatabasePath { get; }

    public AuthService Auth { get; }
    public PremisesService Premises { get; }
    public TenantService Tenants { get; }
    public LeaseService Leases { get; }
    public PaymentService Payments { get; }
    public EmployeeService Employees { get; }
    public EventService Events { get; }
    public ParkingService Parking { get; }
    public ReportService Reports { get; }

    public User? CurrentUser { get; set; }

    public bool IsInRole(params UserRole[] roles) => CurrentUser is not null && roles.Contains(CurrentUser.Role);

    /// <summary>
    /// Автоматичні дії під час запуску: закриття прострочених договорів, позначення минулих заходів
    /// та формування нарахувань за поточний місяць.
    /// </summary>
    public void RunDailyMaintenance()
    {
        Leases.CloseExpired();
        Events.CompletePastEvents();
        Payments.GenerateCharges(Clock.Today.Year, Clock.Today.Month);
    }
}
