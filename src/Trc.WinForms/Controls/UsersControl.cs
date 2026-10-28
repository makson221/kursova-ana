using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Користувачі» (доступний лише адміністратору).</summary>
public partial class UsersControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public UsersControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services) => _services = services;

    public void RefreshData() =>
        UiHelper.Bind(grid, _services.Auth.GetUsers().Select(UserRow.From), r => r.Id);

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new UserEditForm(_services, null);
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<UserRow>(grid, out var row))
            return;

        using var form = new UserEditForm(_services, _services.Auth.Get(row.Id));
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            btnEdit_Click(sender, e);
    }

    private void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<UserRow>(grid, out var row))
            return;

        using var form = new PasswordForm(_services, row.Id, requireCurrent: false);
        if (form.ShowDialog(this) == DialogResult.OK)
            UiHelper.Info(this, $"Пароль користувача {row.Login} змінено.");
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<UserRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Видалити користувача {row.Login}?"))
            return;

        if (UiHelper.Run(this, () => _services.Auth.DeleteUser(row.Id, _services.CurrentUser!.Id)))
            RefreshData();
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && grid.Rows[e.RowIndex].DataBoundItem is UserRow { IsActive: false })
            e.CellStyle!.ForeColor = Color.Gray;
    }

    private void btnRefresh_Click(object sender, EventArgs e) => RefreshData();
}
