using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>
/// Зміна пароля. Користувач змінює власний пароль із підтвердженням поточного;
/// адміністратор може встановити новий пароль іншому користувачу.
/// </summary>
public partial class PasswordForm : Form
{
    private readonly AppServices _services = null!;
    private readonly int _userId;
    private readonly bool _requireCurrent;

    public PasswordForm()
    {
        InitializeComponent();
    }

    public PasswordForm(AppServices services, int userId, bool requireCurrent) : this()
    {
        _services = services;
        _userId = userId;
        _requireCurrent = requireCurrent;

        if (!requireCurrent)
        {
            Text = "Встановлення нового пароля";
            txtCurrent.Enabled = false;
            txtCurrent.PlaceholderText = "не потрібно";
        }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (txtNew.Text != txtConfirm.Text)
        {
            UiHelper.Info(this, "Новий пароль і підтвердження не збігаються.");
            return;
        }

        var saved = UiHelper.Run(this, () =>
        {
            if (_requireCurrent)
                _services.Auth.ChangePassword(_userId, txtCurrent.Text, txtNew.Text);
            else
                _services.Auth.ResetPassword(_userId, txtNew.Text);
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
