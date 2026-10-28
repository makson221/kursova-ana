using System.ComponentModel;
using Trc.Core.Common;
using Trc.Core.Models;
using Trc.Core.Services;

namespace Trc.WinForms.Infrastructure;

// Рядки таблиць: заголовки стовпців задаються атрибутом [DisplayName],
// службові поля приховані атрибутом [Browsable(false)].

public sealed record PremisesRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Номер")] string Number,
    [property: DisplayName("Поверх")] int Floor,
    [property: DisplayName("Тип")] string Type,
    [property: DisplayName("Площа, м²")] decimal Area,
    [property: DisplayName("Ставка, грн/м²")] decimal Rate,
    [property: DisplayName("Оренда на міс., грн")] decimal MonthlyRent,
    [property: DisplayName("Стан")] string StatusName,
    [property: DisplayName("Опис")] string? Description,
    [property: Browsable(false)] PremisesStatus Status)
{
    public static PremisesRow From(Premises p) => new(p.Id, p.Number, p.Floor, p.Type.GetDisplayName(), p.Area,
        p.RatePerSqm, p.BaseMonthlyRent, p.Status.GetDisplayName(), p.Description, p.Status);
}

public sealed record TenantRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Назва")] string Name,
    [property: DisplayName("ЄДРПОУ / РНОКПП")] string TaxCode,
    [property: DisplayName("Категорія")] string Category,
    [property: DisplayName("Контактна особа")] string Contact,
    [property: DisplayName("Телефон")] string Phone,
    [property: DisplayName("E-mail")] string? Email,
    [property: DisplayName("Діючих договорів")] int ActiveLeases)
{
    public static TenantRow From(Tenant t) => new(t.Id, t.CompanyName, t.TaxCode, t.Category.GetDisplayName(),
        t.ContactPerson, t.Phone, t.Email, t.Leases.Count(l => l.Status == LeaseStatus.Active));
}

public sealed record LeaseRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("№ договору")] string Number,
    [property: DisplayName("Орендар")] string Tenant,
    [property: DisplayName("Приміщення")] string Premises,
    [property: DisplayName("Початок")] DateTime StartDate,
    [property: DisplayName("Закінчення")] DateTime EndDate,
    [property: DisplayName("Оренда на міс., грн")] decimal MonthlyRent,
    [property: DisplayName("Депозит, грн")] decimal Deposit,
    [property: DisplayName("Стан")] string StatusName,
    [property: Browsable(false)] LeaseStatus Status,
    [property: Browsable(false)] bool ExpiresSoon)
{
    public static LeaseRow From(Lease l, DateTime today) => new(l.Id, l.Number, l.Tenant.CompanyName,
        $"{l.Premises.Number} ({l.Premises.Area:0.##} м²)", l.StartDate, l.EndDate, l.MonthlyRent, l.Deposit,
        l.Status.GetDisplayName(), l.Status, l.Status == LeaseStatus.Active && l.EndDate <= today.AddDays(30));
}

public sealed record PaymentRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Період")] string Period,
    [property: DisplayName("№ договору")] string Lease,
    [property: DisplayName("Орендар")] string Tenant,
    [property: DisplayName("Приміщення")] string Premises,
    [property: DisplayName("Сума, грн")] decimal Amount,
    [property: DisplayName("Сплатити до")] DateTime DueDate,
    [property: DisplayName("Дата оплати")] DateTime? PaidDate,
    [property: DisplayName("Стан")] string StatusName,
    [property: Browsable(false)] bool IsPaid,
    [property: Browsable(false)] bool IsOverdue)
{
    public static PaymentRow From(Payment p, DateTime today)
    {
        var overdue = p.IsOverdue(today);
        var status = overdue ? $"Прострочено ({(today - p.DueDate.Date).Days} дн.)" : p.Status.GetDisplayName();
        return new(p.Id, $"{p.PeriodMonth:00}.{p.PeriodYear}", p.Lease.Number, p.Lease.Tenant.CompanyName,
            p.Lease.Premises.Number, p.Amount, p.DueDate, p.PaidDate, status, p.Status == PaymentStatus.Paid, overdue);
    }
}

public sealed record EmployeeRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("ПІБ")] string FullName,
    [property: DisplayName("Посада")] string Position,
    [property: DisplayName("Відділ")] string Department,
    [property: DisplayName("Оклад, грн")] decimal Salary,
    [property: DisplayName("Телефон")] string Phone,
    [property: DisplayName("Дата прийому")] DateTime HireDate,
    [property: DisplayName("Стан")] string StatusName,
    [property: Browsable(false)] bool IsActive)
{
    public static EmployeeRow From(Employee e) => new(e.Id, e.FullName, e.Position, e.Department.GetDisplayName(),
        e.Salary, e.Phone, e.HireDate, e.IsActive ? "Працює" : "Звільнений", e.IsActive);
}

public sealed record EventRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Дата і час"), GridFormat("dd.MM.yyyy HH:mm")] DateTime StartTime,
    [property: DisplayName("Назва")] string Title,
    [property: DisplayName("Місце")] string Location,
    [property: DisplayName("Ціна, грн")] decimal Price,
    [property: DisplayName("Місць")] int Capacity,
    [property: DisplayName("Продано")] int Sold,
    [property: DisplayName("Вільно")] int Free,
    [property: DisplayName("Виручка, грн")] decimal Revenue,
    [property: DisplayName("Стан")] string StatusName,
    [property: Browsable(false)] EventStatus Status,
    [property: Browsable(false)] string? Description)
{
    public static EventRow From(EventInfo info)
    {
        var e = info.Event;
        return new(e.Id, e.StartTime, e.Title, e.Location, e.TicketPrice, e.Capacity, info.SoldTickets, info.FreeSeats,
            info.SoldTickets * e.TicketPrice, e.Status.GetDisplayName(), e.Status, e.Description);
    }
}

public sealed record TicketRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Дата продажу"), GridFormat("dd.MM.yyyy HH:mm")] DateTime SoldAt,
    [property: DisplayName("Покупець")] string Buyer,
    [property: DisplayName("Кількість")] int Quantity,
    [property: DisplayName("Сума, грн")] decimal Total,
    [property: DisplayName("Касир")] string? SoldBy)
{
    public static TicketRow From(Ticket t) => new(t.Id, t.SoldAt, t.BuyerName, t.Quantity, t.TotalPrice, t.SoldBy);
}

public sealed record ParkingActiveRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Номер авто")] string Plate,
    [property: DisplayName("В'їзд"), GridFormat("dd.MM.yyyy HH:mm")] DateTime EntryTime,
    [property: DisplayName("Тривалість")] string Duration,
    [property: DisplayName("До сплати, грн")] decimal CurrentFee)
{
    public static ParkingActiveRow From(ParkingSession s, DateTime now, ParkingSettings settings)
    {
        var duration = now - s.EntryTime;
        var text = duration.TotalDays >= 1
            ? $"{(int)duration.TotalDays} д {duration.Hours} год {duration.Minutes} хв"
            : $"{(int)duration.TotalHours} год {duration.Minutes} хв";
        return new(s.Id, s.PlateNumber, s.EntryTime, text, ParkingService.CalculateFee(s.EntryTime, now, settings));
    }
}

public sealed record ParkingHistoryRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Номер авто")] string Plate,
    [property: DisplayName("В'їзд"), GridFormat("dd.MM.yyyy HH:mm")] DateTime EntryTime,
    [property: DisplayName("Виїзд"), GridFormat("dd.MM.yyyy HH:mm")] DateTime? ExitTime,
    [property: DisplayName("Сплачено, грн")] decimal Fee)
{
    public static ParkingHistoryRow From(ParkingSession s) => new(s.Id, s.PlateNumber, s.EntryTime, s.ExitTime, s.Fee ?? 0);
}

public sealed record UserRow(
    [property: Browsable(false)] int Id,
    [property: DisplayName("Логін")] string Login,
    [property: DisplayName("ПІБ")] string FullName,
    [property: DisplayName("Роль")] string Role,
    [property: DisplayName("Стан")] string StatusName,
    [property: Browsable(false)] bool IsActive)
{
    public static UserRow From(User u) => new(u.Id, u.Login, u.FullName, u.Role.GetDisplayName(),
        u.IsActive ? "Активний" : "Заблокований", u.IsActive);
}

public sealed record DebtorRow(
    [property: Browsable(false)] int TenantId,
    [property: DisplayName("Орендар")] string Tenant,
    [property: DisplayName("Телефон")] string Phone,
    [property: DisplayName("Неоплачених місяців")] int Count,
    [property: DisplayName("Борг, грн")] decimal Debt,
    [property: DisplayName("Найстаріший борг з")] DateTime Since)
{
    public static DebtorRow From(DebtorInfo d) => new(d.TenantId, d.TenantName, d.Phone, d.UnpaidCount, d.Debt, d.OldestDueDate);
}

public sealed record UpcomingEventRow(
    [property: DisplayName("Дата і час"), GridFormat("dd.MM.yyyy HH:mm")] DateTime StartTime,
    [property: DisplayName("Захід")] string Title,
    [property: DisplayName("Вільно місць")] int Free);

/// <summary>Елемент випадаючого списку: ідентифікатор і текст.</summary>
public sealed record ListItem(int Id, string Text)
{
    public override string ToString() => Text;
}
