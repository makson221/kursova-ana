namespace Trc.Core.Models;

/// <summary>Орендар — юридична особа або ФОП, що орендує приміщення в ТРЦ.</summary>
public class Tenant
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>Код ЄДРПОУ (8 цифр) або РНОКПП ФОП (10 цифр).</summary>
    public string TaxCode { get; set; } = string.Empty;

    public TenantCategory Category { get; set; }
    public string ContactPerson { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }

    public List<Lease> Leases { get; set; } = new();
}
