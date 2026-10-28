using System.Text.RegularExpressions;

namespace Trc.Core.Common;

/// <summary>Допоміжні перевірки вхідних даних.</summary>
internal static class Guard
{
    public static string Required(string? value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException($"Поле «{field}» є обов'язковим.");
        return value.Trim();
    }

    public static void Positive(decimal value, string field)
    {
        if (value <= 0)
            throw new BusinessException($"Значення поля «{field}» має бути більшим за нуль.");
    }

    public static void NotNegative(decimal value, string field)
    {
        if (value < 0)
            throw new BusinessException($"Значення поля «{field}» не може бути від'ємним.");
    }

    public static void Matches(string value, Regex pattern, string message)
    {
        if (!pattern.IsMatch(value))
            throw new BusinessException(message);
    }
}

internal static partial class Patterns
{
    [GeneratedRegex(@"^(\d{8}|\d{10})$")]
    public static partial Regex TaxCode();

    [GeneratedRegex(@"^\+?[0-9][0-9 ()\-]{8,17}$")]
    public static partial Regex Phone();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex Email();

    [GeneratedRegex(@"^[A-Za-z0-9_.]{3,30}$")]
    public static partial Regex Login();

    /// <summary>Номерний знак України: дві літери, чотири цифри, дві літери (AA1234BB).</summary>
    [GeneratedRegex(@"^[A-Z]{2}\d{4}[A-Z]{2}$")]
    public static partial Regex UaPlate();
}
