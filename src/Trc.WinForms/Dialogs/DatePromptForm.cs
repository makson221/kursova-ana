namespace Trc.WinForms.Dialogs;

/// <summary>Універсальне вікно вибору дати (або місяця).</summary>
public partial class DatePromptForm : Form
{
    public DatePromptForm()
    {
        InitializeComponent();
    }

    /// <param name="title">Заголовок вікна.</param>
    /// <param name="prompt">Пояснення для користувача.</param>
    /// <param name="initial">Початкове значення.</param>
    /// <param name="monthOnly">Вибір лише місяця та року.</param>
    public DatePromptForm(string title, string prompt, DateTime initial, bool monthOnly = false) : this()
    {
        Text = title;
        lblPrompt.Text = prompt;
        if (monthOnly)
        {
            dtValue.Format = DateTimePickerFormat.Custom;
            dtValue.CustomFormat = "MMMM yyyy";
            dtValue.ShowUpDown = true;
        }
        dtValue.Value = initial;
    }

    public DateTime Value => dtValue.Value.Date;

    private void btnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;
}
