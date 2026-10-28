using Trc.Core.Models;
using Trc.Core.Services;

namespace Trc.WinForms.Forms;

/// <summary>Вікно автентифікації користувача.</summary>
public partial class LoginForm : Form
{
    private const int MaxAttempts = 5;

    private readonly AuthService _auth = null!;
    private int _failedAttempts;

    /// <summary>Конструктор для дизайнера Visual Studio.</summary>
    public LoginForm()
    {
        InitializeComponent();
    }

    public LoginForm(AuthService auth) : this()
    {
        _auth = auth;
    }

    /// <summary>Користувач, що успішно увійшов у систему.</summary>
    public User? AuthenticatedUser { get; private set; }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrEmpty(txtPassword.Text))
        {
            lblError.Text = "Введіть логін і пароль.";
            return;
        }

        var user = _auth.Authenticate(txtLogin.Text, txtPassword.Text);
        if (user is null)
        {
            _failedAttempts++;
            if (_failedAttempts >= MaxAttempts)
            {
                MessageBox.Show(this, "Перевищено кількість спроб входу. Програму буде закрито.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                DialogResult = DialogResult.Cancel;
                return;
            }

            lblError.Text = $"Невірний логін або пароль (спроба {_failedAttempts} з {MaxAttempts}).";
            txtPassword.Clear();
            txtPassword.Focus();
            return;
        }

        AuthenticatedUser = user;
        DialogResult = DialogResult.OK;
    }
}
