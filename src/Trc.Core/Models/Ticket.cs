namespace Trc.Core.Models;

/// <summary>Продаж квитків на захід (одна операція може містити кілька квитків).</summary>
public class Ticket
{
    public int Id { get; set; }

    public int MallEventId { get; set; }
    public MallEvent Event { get; set; } = null!;

    public string BuyerName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime SoldAt { get; set; }

    /// <summary>Логін касира, який оформив продаж.</summary>
    public string? SoldBy { get; set; }
}
