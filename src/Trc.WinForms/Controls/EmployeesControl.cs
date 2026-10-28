using Trc.Core.Models;
using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Персонал».</summary>
public partial class EmployeesControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public EmployeesControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services)
    {
        UiHelper.FillEnumFilter<Department>(cmbDepartment);
        _services = services;
    }

    public void RefreshData()
    {
        var list = _services.Employees.GetAll(txtSearch.Text, UiHelper.SelectedFilter<Department>(cmbDepartment),
            includeDismissed: btnShowDismissed.Checked);
        UiHelper.Bind(grid, list.Select(EmployeeRow.From), r => r.Id);

        var active = list.Where(e => e.IsActive).ToList();
        lblSummary.Text = $"Працюючих: {active.Count}   |   Фонд оплати праці: {active.Sum(e => e.Salary):N2} грн/міс.   |   " +
                          $"Середній оклад: {(active.Count == 0 ? 0 : active.Average(e => e.Salary)):N2} грн";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new EmployeeEditForm(_services, null);
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<EmployeeRow>(grid, out var row))
            return;

        using var form = new EmployeeEditForm(_services, _services.Employees.Get(row.Id));
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            btnEdit_Click(sender, e);
    }

    private void btnDismiss_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<EmployeeRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Звільнити працівника {row.FullName}?"))
            return;

        if (UiHelper.Run(this, () => _services.Employees.Dismiss(row.Id)))
            RefreshData();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<EmployeeRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Остаточно видалити запис про працівника {row.FullName}?\n" +
                                    "Для збереження історії краще скористатися командою «Звільнити»."))
            return;

        if (UiHelper.Run(this, () => _services.Employees.Delete(row.Id)))
            RefreshData();
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && grid.Rows[e.RowIndex].DataBoundItem is EmployeeRow { IsActive: false })
            e.CellStyle!.ForeColor = Color.Gray;
    }

    private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void btnShowDismissed_Click(object sender, EventArgs e) => RefreshData();

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => RefreshData();

    private void btnExport_Click(object sender, EventArgs e) => UiHelper.ExportGrid(grid, "Персонал");
}
