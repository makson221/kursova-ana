namespace Trc.Core.Models;

/// <summary>Розважальний захід у ТРЦ (концерт, майстер-клас, кінопоказ тощо).</summary>
public class MallEvent
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public string Location { get; set; } = string.Empty;

    /// <summary>Максимальна кількість місць.</summary>
    public int Capacity { get; set; }

    public decimal TicketPrice { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Planned;

    public List<Ticket> Tickets { get; set; } = new();
}
