namespace Trc.Core.Models;

/// <summary>Перебування автомобіля на паркінгу ТРЦ.</summary>
public class ParkingSession
{
    public int Id { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public decimal? Fee { get; set; }

    public bool IsActive => ExitTime is null;
}
