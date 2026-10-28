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
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => UiHelper.ShowError(null, e.Exception);

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
}
