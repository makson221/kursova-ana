using Trc.Core.Models;
using Trc.WinForms.Dialogs;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Заходи та квитки».</summary>
public partial class EventsControl : UserControl, ISectionControl
{
    private AppServices _services = null!;

    public EventsControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
        UiHelper.SetupGrid(gridTickets);
        grid.SelectionChanged += (_, _) => LoadTickets();
    }

    public void Initialize(AppServices services)
    {
        _services = services;

        // Касир лише продає та повертає квитки.
        var canManage = services.IsInRole(UserRole.Administrator, UserRole.Manager);
        btnAdd.Visible = btnEdit.Visible = btnCancelEvent.Visible = btnDelete.Visible = sep1.Visible = canManage;
        btnUpcoming.Checked = !canManage;
    }

    public void RefreshData()
    {
        var rows = _services.Events.GetAll(includePast: !btnUpcoming.Checked, search: txtSearch.Text)
            .Select(EventRow.From).ToList();
        UiHelper.Bind(grid, rows, r => r.Id);
        LoadTickets();
    }

    private void LoadTickets()
    {
        if (_services is null)
            return;

        var row = UiHelper.Selected<EventRow>(grid);
        if (row is null)
        {
            gridTickets.DataSource = null;
            lblTickets.Text = "Продані квитки";
            return;
        }

        var tickets = _services.Events.GetTickets(row.Id).Select(TicketRow.From).ToList();
        UiHelper.Bind(gridTickets, tickets, t => t.Id);
        lblTickets.Text = $"Продані квитки: «{row.Title}» — {row.Sold} з {row.Capacity}, виручка {row.Revenue:N2} грн";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new EventEditForm(_services, null);
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (!btnEdit.Visible || !UiHelper.TryGetSelected<EventRow>(grid, out var row))
            return;

        using var form = new EventEditForm(_services, _services.Events.Get(row.Id));
        if (form.ShowDialog(this) == DialogResult.OK)
            RefreshData();
    }

    private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            btnSell_Click(sender, e);
    }

    private void btnCancelEvent_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<EventRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Скасувати захід «{row.Title}»? Усі продані квитки ({row.Sold} шт.) " +
                                    "буде повернено покупцям."))
            return;

        UiHelper.Run(this, () =>
        {
            var refunded = _services.Events.Cancel(row.Id);
            UiHelper.Info(this, $"Захід скасовано. Повернено квитків: {refunded}.");
        });
        RefreshData();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<EventRow>(grid, out var row))
            return;
        if (!UiHelper.Confirm(this, $"Видалити захід «{row.Title}»?"))
            return;

        if (UiHelper.Run(this, () => _services.Events.Delete(row.Id)))
            RefreshData();
    }

    private void btnSell_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<EventRow>(grid, out var row))
            return;
        if (row.Status != EventStatus.Planned)
        {
            UiHelper.Info(this, "Продаж квитків на цей захід закрито.");
            return;
        }
        if (row.Free == 0)
        {
            UiHelper.Info(this, "Усі квитки на цей захід продано.");
            return;
        }

        using var form = new TicketSaleForm(_services, row.Id);
        if (form.ShowDialog(this) == DialogResult.OK && form.Ticket is { } ticket)
        {
            UiHelper.Info(this, $"Продано квитків: {ticket.Quantity} на суму {ticket.TotalPrice:N2} грн.");
            RefreshData();
        }
    }

    private void btnRefund_Click(object sender, EventArgs e)
    {
        if (!UiHelper.TryGetSelected<TicketRow>(gridTickets, out var ticket))
            return;
        if (!UiHelper.Confirm(this, $"Оформити повернення {ticket.Quantity} квитк(ів) покупця {ticket.Buyer} " +
                                    $"на суму {ticket.Total:N2} грн?"))
            return;

        if (UiHelper.Run(this, () => _services.Events.RefundTicket(ticket.Id)))
            RefreshData();
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Rows[e.RowIndex].DataBoundItem is not EventRow row)
            return;

        if (row.Status == EventStatus.Cancelled)
            e.CellStyle!.ForeColor = Color.Firebrick;
        else if (row.Status == EventStatus.Completed)
            e.CellStyle!.ForeColor = Color.Gray;
        else if (row.Free == 0)
            e.CellStyle!.ForeColor = Color.DarkOrange;
    }

    private void btnUpcoming_Click(object sender, EventArgs e) => RefreshData();

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (_services is not null) RefreshData();
    }

    private void btnRefresh_Click(object sender, EventArgs e) => RefreshData();
}
