using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Оформлення продажу квитків на захід.</summary>
public partial class TicketSaleForm : Form
{
    private readonly AppServices _services = null!;
    private readonly MallEvent _event = null!;

    public TicketSaleForm()
    {
        InitializeComponent();
    }

    public TicketSaleForm(AppServices services, int eventId) : this()
    {
        _services = services;
        _event = services.Events.Get(eventId);
        var free = _event.Capacity - services.Events.GetSoldCount(eventId);

        lblEvent.Text = _event.Title;
        lblInfo.Text = $"{_event.StartTime:dd.MM.yyyy HH:mm}, {_event.Location}. " +
                       $"Ціна: {_event.TicketPrice:N2} грн. Вільно місць: {free}";
        numQuantity.Maximum = Math.Max(1, Math.Min(20, free));
        UpdateTotal();
    }

    /// <summary>Оформлений продаж.</summary>
    public Ticket? Ticket { get; private set; }

    private void numQuantity_ValueChanged(object sender, EventArgs e) => UpdateTotal();

    private void UpdateTotal()
    {
        if (_event is not null)
            lblTotal.Text = $"До сплати: {_event.TicketPrice * numQuantity.Value:N2} грн";
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        var saved = UiHelper.Run(this, () =>
            Ticket = _services.Events.SellTickets(_event.Id, txtBuyer.Text, (int)numQuantity.Value,
                _services.CurrentUser?.Login));
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
