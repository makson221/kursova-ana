using Trc.Core.Common;
using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Укладання нового договору оренди.</summary>
public partial class LeaseEditForm : Form
{
    private static readonly (string Text, int Months)[] Terms =
    {
        ("6 місяців", 6), ("1 рік", 12), ("2 роки", 24), ("3 роки", 36), ("5 років", 60), ("Інший строк", 0)
    };

    private readonly AppServices _services = null!;
    private readonly List<Premises> _premises = new();

    public LeaseEditForm()
    {
        InitializeComponent();
    }

    public LeaseEditForm(AppServices services) : this()
    {
        _services = services;

        foreach (var tenant in services.Tenants.GetAll())
            cmbTenant.Items.Add(new ListItem(tenant.Id, $"{tenant.CompanyName} ({tenant.TaxCode})"));

        _premises.AddRange(services.Premises.GetAvailable());
        foreach (var p in _premises)
            cmbPremises.Items.Add(new ListItem(p.Id, $"{p.Number} — {p.Type.GetDisplayName()}, {p.Area:0.##} м², поверх {p.Floor}"));

        foreach (var term in Terms)
            cmbTerm.Items.Add(term.Text);

        var today = services.Clock.Today;
        dtStart.Value = new DateTime(today.Year, today.Month, 1).AddMonths(1);
        cmbTerm.SelectedIndex = 1;
        lblNumber.Text = services.Leases.GenerateNumber(dtStart.Value.Year) + " (буде присвоєно автоматично)";

        if (cmbTenant.Items.Count > 0) cmbTenant.SelectedIndex = 0;
        if (cmbPremises.Items.Count > 0) cmbPremises.SelectedIndex = 0;
    }

    private void cmbPremises_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPremises.SelectedItem is not ListItem item)
            return;
        var premises = _premises.First(p => p.Id == item.Id);
        numRent.Value = premises.BaseMonthlyRent;
        numDeposit.Value = premises.BaseMonthlyRent;
    }

    private void dtStart_ValueChanged(object sender, EventArgs e) => ApplyTerm();

    private void cmbTerm_SelectedIndexChanged(object sender, EventArgs e) => ApplyTerm();

    /// <summary>Обчислює дату закінчення за обраним строком оренди.</summary>
    private void ApplyTerm()
    {
        if (cmbTerm.SelectedIndex < 0)
            return;
        var months = Terms[cmbTerm.SelectedIndex].Months;
        dtEnd.Enabled = months == 0;
        if (months > 0)
            dtEnd.Value = dtStart.Value.Date.AddMonths(months).AddDays(-1);
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (cmbTenant.SelectedItem is not ListItem tenant)
        {
            UiHelper.Info(this, "Оберіть орендаря. Якщо список порожній — спочатку додайте орендаря.");
            return;
        }
        if (cmbPremises.SelectedItem is not ListItem premises)
        {
            UiHelper.Info(this, "Немає вільних приміщень для оренди.");
            return;
        }

        var saved = UiHelper.Run(this, () =>
        {
            var lease = _services.Leases.Create(tenant.Id, premises.Id, dtStart.Value, dtEnd.Value,
                numRent.Value, numDeposit.Value);
            UiHelper.Info(this, $"Договір {lease.Number} успішно укладено.");
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
