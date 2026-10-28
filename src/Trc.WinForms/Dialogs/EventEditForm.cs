using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Додавання / редагування розважального заходу.</summary>
public partial class EventEditForm : Form
{
    private readonly AppServices _services = null!;
    private readonly int _id;

    public EventEditForm()
    {
        InitializeComponent();
    }

    public EventEditForm(AppServices services, MallEvent? mallEvent) : this()
    {
        _services = services;

        if (mallEvent is null)
        {
            Text = "Новий захід";
            dtStart.Value = services.Clock.Today.AddDays(7).AddHours(18);
            numCapacity.Value = 100;
            numPrice.Value = 200;
            return;
        }

        _id = mallEvent.Id;
        Text = $"Захід: {mallEvent.Title}";
        txtTitle.Text = mallEvent.Title;
        txtLocation.Text = mallEvent.Location;
        dtStart.Value = mallEvent.StartTime;
        numCapacity.Value = mallEvent.Capacity;
        numPrice.Value = mallEvent.TicketPrice;
        txtDescription.Text = mallEvent.Description;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        var mallEvent = new MallEvent
        {
            Id = _id,
            Title = txtTitle.Text,
            Location = txtLocation.Text,
            StartTime = dtStart.Value,
            Capacity = (int)numCapacity.Value,
            TicketPrice = numPrice.Value,
            Description = txtDescription.Text
        };

        var saved = UiHelper.Run(this, () =>
        {
            if (_id == 0) _services.Events.Add(mallEvent);
            else _services.Events.Update(mallEvent);
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
