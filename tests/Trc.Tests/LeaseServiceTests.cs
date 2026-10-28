using Trc.Core.Common;
using Trc.Core.Models;

namespace Trc.Tests;

public class LeaseServiceTests
{
    [Fact]
    public void Create_CalculatesRentAndOccupiesPremises()
    {
        using var t = new TestDatabase();
        var premisesId = t.AddPremises(area: 120, rate: 650);
        var tenantId = t.AddTenant();

        var lease = t.Leases.Create(tenantId, premisesId, new DateTime(2026, 4, 1), new DateTime(2027, 3, 31));

        Assert.Equal(78_000m, lease.MonthlyRent);
        Assert.Equal("Д-2026-001", lease.Number);
        Assert.Equal(PremisesStatus.Occupied, t.Premises.Get(premisesId).Status);
    }

    [Fact]
    public void Create_RejectsOverlappingLease()
    {
        using var t = new TestDatabase();
        var premisesId = t.AddPremises();
        var first = t.AddTenant("ТОВ «Перший»", "11111111");
        var second = t.AddTenant("ТОВ «Другий»", "22222222");
        t.Leases.Create(first, premisesId, new DateTime(2026, 4, 1), new DateTime(2026, 12, 31));

        var ex = Assert.Throws<BusinessException>(() =>
            t.Leases.Create(second, premisesId, new DateTime(2026, 10, 1), new DateTime(2027, 9, 30)));
        Assert.Contains("вже орендоване", ex.Message);
    }

    [Fact]
    public void Create_RejectsInvalidPeriod()
    {
        using var t = new TestDatabase();
        var premisesId = t.AddPremises();
        var tenantId = t.AddTenant();

        Assert.Throws<BusinessException>(() =>
            t.Leases.Create(tenantId, premisesId, new DateTime(2026, 5, 1), new DateTime(2026, 4, 1)));
        Assert.Throws<BusinessException>(() =>
            t.Leases.Create(tenantId, premisesId, new DateTime(2026, 5, 1), new DateTime(2026, 5, 15)));
    }

    [Fact]
    public void Terminate_FreesPremisesAndRemovesFutureCharges()
    {
        using var t = new TestDatabase();
        var premisesId = t.AddPremises();
        var lease = t.Leases.Create(t.AddTenant(), premisesId, new DateTime(2026, 3, 1), new DateTime(2027, 2, 28));
        t.Payments.GenerateCharges(2026, 3);
        t.Payments.GenerateCharges(2026, 4);
        t.Payments.GenerateCharges(2026, 5);

        t.Leases.Terminate(lease.Id, new DateTime(2026, 4, 20));

        Assert.Equal(LeaseStatus.Terminated, t.Leases.Get(lease.Id).Status);
        Assert.Equal(PremisesStatus.Free, t.Premises.Get(premisesId).Status);
        var months = t.Payments.GetAll().Select(p => p.PeriodMonth).OrderBy(m => m).ToArray();
        Assert.Equal(new[] { 3, 4 }, months);
    }

    [Fact]
    public void CloseExpired_MarksLeaseExpired()
    {
        using var t = new TestDatabase();
        var premisesId = t.AddPremises();
        var lease = t.Leases.Create(t.AddTenant(), premisesId, new DateTime(2026, 3, 1), new DateTime(2026, 5, 31));

        t.Clock.Now = new DateTime(2026, 6, 2);
        Assert.Equal(1, t.Leases.CloseExpired());
        Assert.Equal(LeaseStatus.Expired, t.Leases.Get(lease.Id).Status);
        Assert.Equal(PremisesStatus.Free, t.Premises.Get(premisesId).Status);
    }

    [Fact]
    public void PremisesWithLeases_CannotBeDeleted()
    {
        using var t = new TestDatabase();
        var premisesId = t.AddPremises();
        var tenantId = t.AddTenant();
        t.Leases.Create(tenantId, premisesId, new DateTime(2026, 4, 1), new DateTime(2027, 3, 31));

        Assert.Throws<BusinessException>(() => t.Premises.Delete(premisesId));
        Assert.Throws<BusinessException>(() => t.Tenants.Delete(tenantId));
    }
}
