using Trc.Core.Common;
using Trc.Core.Models;
using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Forms;

/// <summary>Головне вікно: вкладки з розділами системи.</summary>
public partial class MainForm : Form
{
    private readonly AppServices _services = null!;
    private bool _sectionsReady;

    /// <summary>Конструктор для дизайнера Visual Studio.</summary>
    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(AppServices services) : this()
    {
        _services = services;
    }

    /// <summary>Користувач обрав «Змінити користувача».</summary>
    public bool LogoutRequested { get; private set; }

    private void MainForm_Load(object sender, EventArgs e)
    {
        if (_services is null)
            return;

        var user = _services.CurrentUser!;
        Text = $"{UiHelper.AppTitle} — Автоматизована система управління [{user.FullName}]";
        lblUser.Text = $"Користувач: {user.FullName} ({user.Role.GetDisplayName()})";
        lblDb.Text = $"База даних: {Path.GetFileName(_services.DatabasePath)}";

        ApplyRolePermissions(user.Role);

        foreach (TabPage page in tabs.TabPages)
        {
            if (GetSection(page) is { } section)
                section.Initialize(_services);
        }
        _sectionsReady = true;
        LoadTab(tabs.SelectedTab);
    }

    /// <summary>
    /// Розмежування доступу: касир працює лише з продажем квитків і паркінгом,
    /// керування користувачами доступне тільки адміністратору.
    /// </summary>
    private void ApplyRolePermissions(UserRole role)
    {
        var allowed = role switch
        {
            UserRole.Administrator => tabs.TabPages.Cast<TabPage>().ToList(),
            UserRole.Manager => tabs.TabPages.Cast<TabPage>().Where(p => p != tabUsers).ToList(),
            _ => new List<TabPage> { tabDashboard, tabEvents, tabParking }
        };

        foreach (var page in tabs.TabPages.Cast<TabPage>().Except(allowed).ToList())
        {
            tabs.TabPages.Remove(page);
            page.Dispose();
        }
    }

    private static ISectionControl? GetSection(TabPage? page) =>
        page?.Controls.OfType<ISectionControl>().FirstOrDefault();

    /// <summary>Дані вкладки перечитуються з бази при кожному переході на неї.</summary>
    private void LoadTab(TabPage? page)
    {
        if (page is null || GetSection(page) is not { } section)
            return;

        Cursor = Cursors.WaitCursor;
        try
        {
            UiHelper.Run(this, section.RefreshData);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void tabs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_sectionsReady)
            LoadTab(tabs.SelectedTab);
    }

    private void mnuChangePassword_Click(object sender, EventArgs e)
    {
        using var form = new PasswordForm(_services, _services.CurrentUser!.Id, requireCurrent: true);
        if (form.ShowDialog(this) == DialogResult.OK)
            UiHelper.Info(this, "Пароль успішно змінено.");
    }

    private void mnuLogout_Click(object sender, EventArgs e)
    {
        LogoutRequested = true;
        Close();
    }

    private void mnuExit_Click(object sender, EventArgs e) => Close();

    private void mnuAbout_Click(object sender, EventArgs e)
    {
        UiHelper.Info(this,
            "Автоматизована система управління торгово-розважальним центром." + Environment.NewLine +
            Environment.NewLine +
            "Функції: облік приміщень, орендарів і договорів оренди, нарахування та контроль орендних платежів, " +
            "облік персоналу, розважальні заходи та продаж квитків, паркінг, аналітичні звіти." + Environment.NewLine +
            Environment.NewLine +
            "Курсовий проєкт. C#, .NET 8, Windows Forms, Entity Framework Core, SQLite.");
    }
}
