namespace Trc.Core.Models;

/// <summary>Приміщення (торгова площа) ТРЦ.</summary>
public class Premises
{
    public int Id { get; set; }

    /// <summary>Номер приміщення, наприклад «1-015».</summary>
    public string Number { get; set; } = string.Empty;

    public int Floor { get; set; }

    /// <summary>Площа, м².</summary>
    public decimal Area { get; set; }

    public PremisesType Type { get; set; }

    /// <summary>Орендна ставка, грн за м² на місяць.</summary>
    public decimal RatePerSqm { get; set; }

    public PremisesStatus Status { get; set; } = PremisesStatus.Free;
    public string? Description { get; set; }

    public List<Lease> Leases { get; set; } = new();

    /// <summary>Базова місячна орендна плата (площа × ставка).</summary>
    public decimal BaseMonthlyRent => Math.Round(Area * RatePerSqm, 2);
}
