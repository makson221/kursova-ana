namespace Trc.Core.Models;

/// <summary>Працівник служб ТРЦ.</summary>
public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public Department Department { get; set; }

    /// <summary>Посадовий оклад, грн на місяць.</summary>
    public decimal Salary { get; set; }

    public string Phone { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; } = true;
}
