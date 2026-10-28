using Microsoft.Data.Sqlite;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;
using Trc.Core.Services;

namespace Trc.Tests;

/// <summary>Окрема база SQLite в пам'яті для кожного тесту.</summary>
public sealed class TestDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public TestDatabase(bool seedDemoData = false)
    {
        (Factory, _connection) = TrcDbContextFactory.InMemory();
        Clock = new FixedClock(new DateTime(2026, 3, 15, 12, 0, 0));
        using var db = Factory();
        DbInitializer.Initialize(db, Clock, seedDemoData);
    }

    public Func<TrcDbContext> Factory { get; }
    public FixedClock Clock { get; }

    public AuthService Auth => new(Factory, Clock);
    public PremisesService Premises => new(Factory, Clock);
    public TenantService Tenants => new(Factory, Clock);
    public LeaseService Leases => new(Factory, Clock);
    public PaymentService Payments => new(Factory, Clock);
    public EmployeeService Employees => new(Factory, Clock);
    public EventService Events => new(Factory, Clock);
    public ParkingService Parking => new(Factory, Clock, new ParkingSettings { Capacity = 2 });
    public ReportService Reports => new(Factory, Clock);

    public int AddPremises(string number = "1-001", decimal area = 100, decimal rate = 500) =>
        Premises.Add(new Premises { Number = number, Floor = 1, Area = area, RatePerSqm = rate, Type = PremisesType.Shop });

    public int AddTenant(string name = "ТОВ «Тест»", string code = "12345678") =>
        Tenants.Add(new Tenant
        {
            CompanyName = name, TaxCode = code, Category = TenantCategory.Clothing,
            ContactPerson = "Іван Іванов", Phone = "+380671234567"
        });

    public void Dispose() => _connection.Dispose();
}
