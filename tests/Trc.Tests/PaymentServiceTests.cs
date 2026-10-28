using Trc.Core.Common;
using Trc.Core.Models;

namespace Trc.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void GenerateCharges_IsIdempotent()
    {
        using var t = new TestDatabase();
        t.Leases.Create(t.AddTenant(), t.AddPremises(), new DateTime(2026, 1, 1), new DateTime(2026, 12, 31));

        Assert.Equal(1, t.Payments.GenerateCharges(2026, 3));
        Assert.Equal(0, t.Payments.GenerateCharges(2026, 3));
        Assert.Single(t.Payments.GetAll());
    }

    [Fact]
    public void GenerateCharges_SkipsMonthsOutsideLease()
    {
        using var t = new TestDatabase();
        t.Leases.Create(t.AddTenant(), t.AddPremises(), new DateTime(2026, 4, 1), new DateTime(2026, 9, 30));

        Assert.Equal(0, t.Payments.GenerateCharges(2026, 3));
        Assert.Equal(0, t.Payments.GenerateCharges(2026, 10));
        Assert.Equal(1, t.Payments.GenerateCharges(2026, 4));
    }

    [Fact]
    public void OverduePayments_AppearInDebtors()
    {
        using var t = new TestDatabase();
        t.Leases.Create(t.AddTenant(), t.AddPremises(area: 100, rate: 500), new DateTime(2026, 1, 1), new DateTime(2026, 12, 31));
        t.Payments.GenerateCharges(2026, 2);
        t.Payments.GenerateCharges(2026, 3);

        var debtor = Assert.Single(t.Payments.GetDebtors());
        Assert.Equal(2, debtor.UnpaidCount);   // 10.02 та 10.03 — обидва прострочені на 15.03
        Assert.Equal(100_000m, debtor.Debt);

        var february = t.Payments.GetAll().Single(p => p.PeriodMonth == 2);
        t.Payments.MarkPaid(february.Id);
        Assert.Equal(50_000m, t.Payments.GetTotalDebt());
        Assert.Throws<BusinessException>(() => t.Payments.MarkPaid(february.Id));
    }

    [Fact]
    public void Filter_ReturnsOnlyRequestedPayments()
    {
        using var t = new TestDatabase();
        t.Leases.Create(t.AddTenant(), t.AddPremises(), new DateTime(2026, 1, 1), new DateTime(2026, 12, 31));
        t.Payments.GenerateCharges(2026, 3);
        t.Payments.GenerateCharges(2026, 4);
        var march = t.Payments.GetAll(month: 3).Single();
        t.Payments.MarkPaid(march.Id);

        Assert.Single(t.Payments.GetAll(PaymentFilter.Paid));
        Assert.Single(t.Payments.GetAll(PaymentFilter.Unpaid));
        Assert.Empty(t.Payments.GetAll(PaymentFilter.Overdue)); // квітневий ще не прострочений
    }
}
