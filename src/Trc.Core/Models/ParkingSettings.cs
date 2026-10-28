namespace Trc.Core.Models;

/// <summary>Тарифи та параметри паркінгу.</summary>
public record ParkingSettings
{
    /// <summary>Кількість машиномісць.</summary>
    public int Capacity { get; init; } = 250;

    /// <summary>Вартість кожної розпочатої години, грн.</summary>
    public decimal HourlyRate { get; init; } = 20m;

    /// <summary>Безкоштовний час стоянки, хв.</summary>
    public int FreeMinutes { get; init; } = 15;

    /// <summary>Максимальна плата за добу, грн.</summary>
    public decimal DailyMaximum { get; init; } = 200m;
}
