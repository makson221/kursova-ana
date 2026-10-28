using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Додавання / редагування облікового запису користувача.</summary>
public partial class UserEditForm : Form
{
    private readonly AppServices _services = null!;
    private readonly int _id;

    public UserEditForm()
    {
        InitializeComponent();
    }

    public UserEditForm(AppServices services, User? user) : this()
    {
        _services = services;
        UiHelper.FillEnum(cmbRole, user?.Role ?? UserRole.Cashier);

        if (user is null)
        {
            Text = "Новий користувач";
            chkActive.Checked = true;
            return;
        }

        _id = user.Id;
        Text = $"Користувач: {user.Login}";
        txtLogin.Text = user.Login;
        txtLogin.ReadOnly = true;
        txtName.Text = user.FullName;
        chkActive.Checked = user.IsActive;
        // Пароль змінюється окремою командою «Змінити пароль».
        txtPassword.Enabled = false;
        txtConfirm.Enabled = false;
        txtPassword.PlaceholderText = "без змін";
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        var role = UiHelper.SelectedEnum<UserRole>(cmbRole);
        if (_id == 0 && txtPassword.Text != txtConfirm.Text)
        {
            UiHelper.Info(this, "Паролі не збігаються.");
            return;
        }

        var saved = UiHelper.Run(this, () =>
        {
            if (_id == 0)
                _services.Auth.AddUser(txtLogin.Text, txtName.Text, role, txtPassword.Text);
            else
                _services.Auth.UpdateUser(_id, txtName.Text, role, chkActive.Checked);
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
