using System.Globalization;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.WinForms.Forms;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms;

internal static class Program
{
    /// <summary>Точка входу застосунку.</summary>
    [STAThread]
    private static void Main()
    {
        // Українські формати чисел і дат незалежно від налаштувань Windows.
        var culture = CultureInfo.GetCultureInfo("uk-UA");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => UiHelper.ShowError(null, e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => LogFatalError(e.ExceptionObject);

        AppServices services;
        try
        {
            var databasePath = Path.Combine(AppContext.BaseDirectory, "trc.db");
            var factory = TrcDbContextFactory.ForFile(databasePath);
            var clock = new SystemClock();
            using (var db = factory())
            {
                DbInitializer.Initialize(db, clock, seedDemoData: true);
            }

            services = new AppServices(factory, clock, databasePath);
            services.RunDailyMaintenance();
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(null, ex, "Не вдалося відкрити базу даних.");
            return;
        }

        // Цикл дозволяє змінити користувача без перезапуску програми.
        while (true)
        {
            using var login = new LoginForm(services.Auth);
            if (login.ShowDialog() != DialogResult.OK || login.AuthenticatedUser is null)
                return;

            services.CurrentUser = login.AuthenticatedUser;
            using var main = new MainForm(services);
            Application.Run(main);
            if (!main.LogoutRequested)
                return;
        }
    }

    /// <summary>Записує неперехоплену помилку у файл error.log поруч із програмою.</summary>
    private static void LogFatalError(object exception)
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "error.log");
            File.AppendAllText(path, $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] {exception}{Environment.NewLine}{Environment.NewLine}");
        }
        catch
        {
            // Якщо записати журнал неможливо, нічого не робимо: програма вже аварійно завершується.
        }
    }
}
