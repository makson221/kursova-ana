using Trc.Core.Models;
using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Договори оренди».</summary>
public partial class LeasesControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public LeasesControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services)
    {
        UiHelper.FillEnumFilter<LeaseStatus>(cmbStatus);
        cmbStatus.SelectedIndex = 1; // за замовчуванням — діючі
        _services = services;
    }

    public void RefreshData()
    {
        var today = _services.Clock.Today;
        var leases = _services.Leases.GetAll(UiHelper.SelectedFilter<LeaseStatus>(cmbStatus), txtSearch.Text);
        var rows = leases.Select(l => LeaseRow.From(l, today)).ToList();
        UiHelper.Bind(grid, rows, r => r.Id);

        var active = leases.Where(l => l.Status == LeaseStatus.Active).ToList();
        lblSummary.Text = $"Договорів: {rows.Count}   |   Діючих: {active.Count}   |   " +
                          $"Щомісячний дохід від оренди: {active.Sum(l => l.MonthlyRent):N2} грн   |   " +
                          $"Закінчуються протягом 30 днів: {rows.Count(r => r.ExpiresSoon)}";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new LeaseEditForm(_services);
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void btnExtend_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<LeaseRow>(grid, out var row))
            return;

        using var form = new DatePromptForm("Продовження договору",
            $"Договір {row.Number} діє до {row.EndDate:dd.MM.yyyy}.\nОберіть нову дату закінчення:",
            row.EndDate.AddYears(1));
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        if (UiHelper.Run(this, () => _services.Leases.Extend(row.Id, form.Value)))
            RefreshData();
    }

    private void btnTerminate_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<LeaseRow>(grid, out var row))
            return;

        using var form = new DatePromptForm("Розірвання договору",
            $"Дата розірвання договору {row.Number} ({row.Tenant}):", _services.Clock.Today);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;
        if (!UiHelper.Confirm(this, $"Розірвати договір {row.Number}? Приміщення буде звільнено, " +
                                    "а неоплачені нарахування за наступні місяці — анульовано."))
            return;

        if (UiHelper.Run(this, () => _services.Leases.Terminate(row.Id, form.Value)))
            RefreshData();
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Rows[e.RowIndex].DataBoundItem is not LeaseRow row)
            return;

        if (row.Status != LeaseStatus.Active)
            e.CellStyle!.ForeColor = Color.Gray;
        else if (row.ExpiresSoon)
            e.CellStyle!.ForeColor = Color.DarkOrange;
    }

    private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => RefreshData();

    private void btnExport_Click(object sender, EventArgs e) => UiHelper.ExportGrid(grid, "Договори");
}
