using Trc.Core.Common;
using Trc.Core.Models;
using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Платежі»: нарахування орендної плати та реєстрація оплат.</summary>
public partial class PaymentsControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public PaymentsControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services)
    {
        cmbFilter.Items.Clear();
        foreach (var option in EnumHelper.GetOptions<PaymentFilter>())
            cmbFilter.Items.Add(option);
        cmbFilter.SelectedIndex = (int)PaymentFilter.Unpaid;
        _services = services;
    }

    private PaymentFilter CurrentFilter =>
        cmbFilter.SelectedItem is EnumOption<PaymentFilter> option ? option.Value : PaymentFilter.All;

    public void RefreshData()
    {
        var today = _services.Clock.Today;
        var rows = _services.Payments.GetAll(CurrentFilter, search: txtSearch.Text)
            .Select(p => PaymentRow.From(p, today)).ToList();
        UiHelper.Bind(grid, rows, r => r.Id);

        lblSummary.Text = $"Записів: {rows.Count}   |   Сума: {rows.Sum(r => r.Amount):N2} грн   |   " +
                          $"Сплачено: {rows.Where(r => r.IsPaid).Sum(r => r.Amount):N2} грн   |   " +
                          $"Прострочено: {rows.Where(r => r.IsOverdue).Sum(r => r.Amount):N2} грн";
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        using var form = new DatePromptForm("Формування нарахувань",
            "Оберіть місяць, за який потрібно нарахувати орендну плату за всіма діючими договорами:",
            _services.Clock.Today, monthOnly: true);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        UiHelper.Run(this, () =>
        {
            var count = _services.Payments.GenerateCharges(form.Value.Year, form.Value.Month);
            UiHelper.Info(this, count == 0
                ? "Нових нарахувань немає — за цей місяць усі нарахування вже сформовано."
                : $"Сформовано нарахувань: {count}.");
        });
        RefreshData();
    }

    private void btnPay_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<PaymentRow>(grid, out var row))
            return;
        if (row.IsPaid)
        {
            UiHelper.Info(this, "Цей платіж уже сплачено.");
            return;
        }

        using var form = new DatePromptForm("Реєстрація оплати",
            $"{row.Tenant}, період {row.Period}, сума {row.Amount:N2} грн.\nДата надходження коштів:",
            _services.Clock.Today);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        if (UiHelper.Run(this, () => _services.Payments.MarkPaid(row.Id, form.Value)))
            RefreshData();
    }

    private void btnCancelPay_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<PaymentRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Скасувати оплату за період {row.Period} ({row.Tenant})?"))
            return;

        if (UiHelper.Run(this, () => _services.Payments.CancelPayment(row.Id)))
            RefreshData();
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Rows[e.RowIndex].DataBoundItem is not PaymentRow row)
            return;

        if (row.IsOverdue)
            e.CellStyle!.ForeColor = Color.Firebrick;
        else if (row.IsPaid)
            e.CellStyle!.ForeColor = Color.SeaGreen;
    }

    private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => RefreshData();

    private void btnExport_Click(object sender, EventArgs e) => UiHelper.ExportGrid(grid, "Платежі");
}
