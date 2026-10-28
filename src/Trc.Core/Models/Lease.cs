namespace Trc.Core.Models;

/// <summary>Договір оренди приміщення.</summary>
public class Lease
{
    public int Id { get; set; }

    /// <summary>Номер договору, наприклад «Д-2026-001».</summary>
    public string Number { get; set; } = string.Empty;

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public int PremisesId { get; set; }
    public Premises Premises { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    /// <summary>Щомісячна орендна плата, грн.</summary>
    public decimal MonthlyRent { get; set; }

    /// <summary>Гарантійний депозит, грн.</summary>
    public decimal Deposit { get; set; }

    public LeaseStatus Status { get; set; } = LeaseStatus.Active;

    public List<Payment> Payments { get; set; } = new();
}
