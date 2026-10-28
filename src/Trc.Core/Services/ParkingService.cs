using System.Text;
using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Облік в'їзду/виїзду автомобілів і розрахунок плати за паркування.</summary>
public class ParkingService : ServiceBase
{
    public ParkingService(Func<TrcDbContext> contextFactory, IClock clock, ParkingSettings? settings = null)
        : base(contextFactory, clock)
    {
        Settings = settings ?? new ParkingSettings();
    }

    public ParkingSettings Settings { get; }

    /// <summary>
    /// Розрахунок плати: перші <see cref="ParkingSettings.FreeMinutes"/> хв — безкоштовно,
    /// далі оплачується кожна розпочата година, але не більше добового максимуму за кожну добу.
    /// </summary>
    public static decimal CalculateFee(DateTime entry, DateTime exit, ParkingSettings settings)
    {
        if (exit < entry)
            throw new BusinessException("Час виїзду не може бути раніше часу в'їзду.");

        var duration = exit - entry;
        if (duration.TotalMinutes <= settings.FreeMinutes)
            return 0;

        var fullDays = (int)duration.TotalDays;
        var remainder = duration - TimeSpan.FromDays(fullDays);
        var hours = (int)Math.Ceiling(remainder.TotalHours);
        var remainderFee = Math.Min(hours * settings.HourlyRate, settings.DailyMaximum);
        return fullDays * settings.DailyMaximum + remainderFee;
    }

    /// <summary>
    /// Приводить номерний знак до єдиного вигляду: верхній регістр, без пробілів і дефісів,
    /// кириличні літери, що збігаються за написанням із латинськими, замінюються латинськими.
    /// </summary>
    public static string NormalizePlate(string plate)
    {
        const string cyrillic = "АВЕІКМНОРСТХ";
        const string latin = "ABEIKMHOPCTX";

        var sb = new StringBuilder();
        foreach (var ch in plate.Trim().ToUpperInvariant())
        {
            if (ch is ' ' or '-') continue;
            var index = cyrillic.IndexOf(ch);
            sb.Append(index >= 0 ? latin[index] : ch);
        }
        return sb.ToString();
    }

    public static bool IsValidPlate(string plate) => Patterns.UaPlate().IsMatch(NormalizePlate(plate));

    public List<ParkingSession> GetActive()
    {
        using var db = ContextFactory();
        return db.ParkingSessions.AsNoTracking().Where(s => s.ExitTime == null).OrderBy(s => s.EntryTime).ToList();
    }

    public List<ParkingSession> GetHistory(DateTime from, DateTime to, string? plate = null)
    {
        using var db = ContextFactory();
        var start = from.Date;
        var end = to.Date.AddDays(1);
        var query = db.ParkingSessions.AsNoTracking()
            .Where(s => s.ExitTime != null && s.EntryTime >= start && s.EntryTime < end);
        if (!string.IsNullOrWhiteSpace(plate))
        {
            var normalized = NormalizePlate(plate);
            query = query.Where(s => s.PlateNumber.Contains(normalized));
        }
        return query.OrderByDescending(s => s.EntryTime).ToList();
    }

    public int GetFreeSpaces()
    {
        using var db = ContextFactory();
        return Settings.Capacity - db.ParkingSessions.Count(s => s.ExitTime == null);
    }

    /// <summary>Реєстрація в'їзду автомобіля.</summary>
    public ParkingSession RegisterEntry(string plate)
    {
        var normalized = NormalizePlate(Guard.Required(plate, "Номерний знак"));
        Guard.Matches(normalized, Patterns.UaPlate(), "Невірний формат номерного знака. Приклад: AA1234BB.");

        using var db = ContextFactory();
        if (db.ParkingSessions.Any(s => s.PlateNumber == normalized && s.ExitTime == null))
            throw new BusinessException($"Автомобіль {normalized} уже перебуває на паркінгу.");
        if (db.ParkingSessions.Count(s => s.ExitTime == null) >= Settings.Capacity)
            throw new BusinessException("Вільних місць на паркінгу немає.");

        var session = new ParkingSession { PlateNumber = normalized, EntryTime = Clock.Now };
        db.ParkingSessions.Add(session);
        db.SaveChanges();
        return session;
    }

    /// <summary>Попередній розрахунок плати на поточний момент (без реєстрації виїзду).</summary>
    public decimal PreviewFee(int sessionId)
    {
        using var db = ContextFactory();
        var session = db.ParkingSessions.Find(sessionId) ?? throw new BusinessException("Запис не знайдено.");
        return CalculateFee(session.EntryTime, session.ExitTime ?? Clock.Now, Settings);
    }

    /// <summary>Реєстрація виїзду автомобіля з розрахунком плати.</summary>
    public ParkingSession RegisterExit(int sessionId)
    {
        using var db = ContextFactory();
        var session = db.ParkingSessions.Find(sessionId) ?? throw new BusinessException("Запис не знайдено.");
        if (session.ExitTime is not null)
            throw new BusinessException("Виїзд цього автомобіля вже зареєстровано.");

        session.ExitTime = Clock.Now;
        session.Fee = CalculateFee(session.EntryTime, session.ExitTime.Value, Settings);
        db.SaveChanges();
        return session;
    }
}
