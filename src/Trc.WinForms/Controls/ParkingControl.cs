using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Паркінг»: в'їзд і виїзд автомобілів, оплата, історія.</summary>
public partial class ParkingControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public ParkingControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(gridActive);
        UiHelper.SetupGrid(gridHistory);
    }

    public void Initialize(AppServices services)
    {
        _services = services;
        var today = services.Clock.Today;
        dtFrom.Value = today.AddDays(-7);
        dtTo.Value = today;

        var s = services.Parking.Settings;
        lblTariff.Text = $"Тариф: перші {s.FreeMinutes} хв — безкоштовно, далі {s.HourlyRate:N0} грн за кожну розпочату годину, " +
                         $"не більше {s.DailyMaximum:N0} грн за добу. Місць: {s.Capacity}.";
        timer.Start();
    }

    public void RefreshData()
    {
        RefreshActive();
        RefreshHistory();
    }

    private void RefreshActive()
    {
        var now = _services.Clock.Now;
        var settings = _services.Parking.Settings;
        var rows = _services.Parking.GetActive().Select(s => ParkingActiveRow.From(s, now, settings)).ToList();
        UiHelper.Bind(gridActive, rows, r => r.Id);

        var free = settings.Capacity - rows.Count;
        lblFreeSpaces.Text = $"Вільно місць: {free} з {settings.Capacity}";
        lblFreeSpaces.ForeColor = free > settings.Capacity / 10 ? Color.SeaGreen : Color.Firebrick;
        grpActive.Text = $"Автомобілі на паркінгу ({rows.Count})";
    }

    private void RefreshHistory()
    {
        var rows = _services.Parking.GetHistory(dtFrom.Value, dtTo.Value, txtHistoryPlate.Text)
            .Select(ParkingHistoryRow.From).ToList();
        UiHelper.Bind(gridHistory, rows, r => r.Id);
        lblHistoryTotal.Text = $"Заїздів: {rows.Count}   |   Виручка: {rows.Sum(r => r.Fee):N2} грн";
    }

    private void btnEntry_Click(object sender, EventArgs e)
    {
        var registered = UiHelper.Run(this, () =>
        {
            var session = _services.Parking.RegisterEntry(txtPlate.Text);
            UiHelper.Info(this, $"В'їзд автомобіля {session.PlateNumber} зареєстровано о {session.EntryTime:HH:mm}.");
        });
        if (!registered)
            return;

        txtPlate.Clear();
        txtPlate.Focus();
        RefreshActive();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<ParkingActiveRow>(gridActive, out var row))
            return;

        var fee = _services.Parking.PreviewFee(row.Id);
        var question = $"Автомобіль {row.Plate}\nЧас стоянки: {row.Duration}\n\n" +
                       (fee == 0 ? "Стоянка безкоштовна." : $"До сплати: {fee:N2} грн.") +
                       "\n\nЗареєструвати виїзд?";
        if (!UiHelper.Confirm(this, question))
            return;

        if (UiHelper.Run(this, () => _services.Parking.RegisterExit(row.Id)))
            RefreshData();
    }

    private void gridActive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            btnExit_Click(sender, e);
    }

    private void btnRefreshActive_Click(object sender, EventArgs e) => RefreshActive();

    private void btnShowHistory_Click(object sender, EventArgs e) => UiHelper.Run(this, RefreshHistory);

    /// <summary>Періодичне оновлення тривалості стоянки та поточної вартості.</summary>
    private void timer_Tick(object sender, EventArgs e)
    {
        if (_services is not null && Visible)
            UiHelper.Run(this, RefreshActive);
    }
}
