using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Головна панель з ключовими показниками роботи ТРЦ.</summary>
public partial class DashboardControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public DashboardControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(gridDebtors);
        UiHelper.SetupGrid(gridEvents);
    }

    public void Initialize(AppServices services)
    {
        _services = services;
        // Касиру фінансова інформація про оренду не показується.
        split.Panel1Collapsed = !services.IsInRole(UserRole.Administrator, UserRole.Manager);
    }

    public void RefreshData()
    {
        var s = _services.Reports.GetDashboard();
        var culture = System.Globalization.CultureInfo.GetCultureInfo("uk-UA");
        lblDate.Text = _services.Clock.Now.ToString("dddd, d MMMM yyyy", culture);

        flowCards.SuspendLayout();
        flowCards.Controls.Clear();
        AddCard("Заповненість ТРЦ", $"{s.OccupancyPercent:0.0}%",
            $"{s.PremisesOccupied} з {s.PremisesTotal} приміщень", Color.SteelBlue);
        AddCard("Орендована площа", $"{s.OccupiedArea:N0} м²", $"із {s.TotalArea:N0} м²", Color.SteelBlue);

        if (_services.IsInRole(UserRole.Administrator, UserRole.Manager))
        {
            AddCard("Діючі договори", s.ActiveLeases.ToString(), $"орендарів: {s.ActiveTenants}", Color.SeaGreen);
            AddCard("Дохід від оренди", $"{s.MonthlyRentIncome:N0} грн", "щомісяця за договорами", Color.SeaGreen);
            AddCard("Заборгованість", $"{s.TotalDebt:N0} грн", $"боржників: {s.DebtorsCount}",
                s.TotalDebt > 0 ? Color.Firebrick : Color.SeaGreen);
            AddCard("Договори закінчуються", s.LeasesExpiringSoon.ToString(), "протягом 30 днів",
                s.LeasesExpiringSoon > 0 ? Color.DarkOrange : Color.SeaGreen);
            AddCard("Персонал", s.Employees.ToString(), "працюючих співробітників", Color.SlateGray);
        }

        AddCard("Паркінг", $"{s.ParkingOccupied} / {s.ParkingCapacity}", "зайнято місць", Color.SlateGray);
        AddCard("Заходи", s.UpcomingEvents.ToString(), "заплановано", Color.MediumPurple);
        flowCards.ResumeLayout();

        UiHelper.Bind(gridDebtors, _services.Payments.GetDebtors().Select(DebtorRow.From));
        UiHelper.Bind(gridEvents, _services.Events.GetAll(includePast: false)
            .Where(x => x.Event.Status == EventStatus.Planned)
            .Take(10)
            .Select(x => new UpcomingEventRow(x.Event.StartTime, x.Event.Title, x.FreeSeats)));
    }

    /// <summary>Картка показника: назва, значення та підпис.</summary>
    private void AddCard(string caption, string value, string note, Color accent)
    {
        var card = new Panel
        {
            Size = new Size(220, 95),
            Margin = new Padding(6),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };
        card.Controls.Add(new Label
        {
            Text = note, ForeColor = Color.DimGray, AutoSize = false,
            Location = new Point(10, 68), Size = new Size(200, 18)
        });
        card.Controls.Add(new Label
        {
            Text = value, ForeColor = accent, Font = new Font(Font.FontFamily, 16f, FontStyle.Bold),
            AutoSize = false, Location = new Point(8, 30), Size = new Size(205, 34)
        });
        card.Controls.Add(new Label
        {
            Text = caption, Font = new Font(Font.FontFamily, 9f, FontStyle.Bold),
            AutoSize = false, Location = new Point(10, 8), Size = new Size(200, 20)
        });
        card.Controls.Add(new Panel { BackColor = accent, Dock = DockStyle.Left, Width = 4 });
        flowCards.Controls.Add(card);
    }

    private void btnRefresh_Click(object sender, EventArgs e) => UiHelper.Run(this, RefreshData);
}
