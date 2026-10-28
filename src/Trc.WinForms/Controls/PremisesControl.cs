using Trc.Core.Models;
using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Приміщення».</summary>
public partial class PremisesControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public PremisesControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services)
    {
        UiHelper.FillEnumFilter<PremisesStatus>(cmbStatus);
        _services = services;
    }

    public void RefreshData()
    {
        var list = _services.Premises.GetAll(txtSearch.Text, UiHelper.SelectedFilter<PremisesStatus>(cmbStatus));
        UiHelper.Bind(grid, list.Select(PremisesRow.From), r => r.Id);

        var free = list.Count(p => p.Status == PremisesStatus.Free);
        lblSummary.Text = $"Приміщень: {list.Count}   |   Загальна площа: {list.Sum(p => p.Area):N2} м²   |   " +
                          $"Вільних: {free}   |   Площа вільних: {list.Where(p => p.Status == PremisesStatus.Free).Sum(p => p.Area):N2} м²";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new PremisesEditForm(_services, null);
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<PremisesRow>(grid, out var row))
            return;

        using var form = new PremisesEditForm(_services, _services.Premises.Get(row.Id));
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            btnEdit_Click(sender, e);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<PremisesRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Видалити приміщення {row.Number}?"))
            return;

        if (UiHelper.Run(this, () => _services.Premises.Delete(row.Id)))
            RefreshData();
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Rows[e.RowIndex].DataBoundItem is not PremisesRow row)
            return;

        e.CellStyle!.ForeColor = row.Status switch
        {
            PremisesStatus.Free => Color.SeaGreen,
            PremisesStatus.Maintenance => Color.DarkOrange,
            _ => grid.DefaultCellStyle.ForeColor
        };
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

    private void btnExport_Click(object sender, EventArgs e) => UiHelper.ExportGrid(grid, "Приміщення");
}
