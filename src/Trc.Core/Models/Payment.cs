namespace Trc.Core.Models;

/// <summary>Нарахування орендної плати за певний місяць.</summary>
public class Payment
{
    public int Id { get; set; }

    public int LeaseId { get; set; }
    public Lease Lease { get; set; } = null!;

    public int PeriodYear { get; set; }
    public int PeriodMonth { get; set; }

    public decimal Amount { get; set; }

    /// <summary>Кінцевий термін оплати.</summary>
    public DateTime DueDate { get; set; }

    public DateTime? PaidDate { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    /// <summary>Чи прострочено платіж на вказану дату.</summary>
    public bool IsOverdue(DateTime today) => Status == PaymentStatus.Pending && DueDate.Date < today.Date;
}
