using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Орендарі».</summary>
public partial class TenantsControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public TenantsControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services) => _services = services;

    public void RefreshData()
    {
        var rows = _services.Tenants.GetAll(txtSearch.Text).Select(TenantRow.From).ToList();
        UiHelper.Bind(grid, rows, r => r.Id);
        lblSummary.Text = $"Орендарів: {rows.Count}   |   З діючими договорами: {rows.Count(r => r.ActiveLeases > 0)}";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new TenantEditForm(_services, null);
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<TenantRow>(grid, out var row))
            return;

        using var form = new TenantEditForm(_services, _services.Tenants.Get(row.Id));
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
        if (!UiHelper.TryGetSelected<TenantRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Видалити орендаря «{row.Name}»?"))
            return;

        if (UiHelper.Run(this, () => _services.Tenants.Delete(row.Id)))
            RefreshData();
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => RefreshData();

    private void btnExport_Click(object sender, EventArgs e) => UiHelper.ExportGrid(grid, "Орендарі");
}
