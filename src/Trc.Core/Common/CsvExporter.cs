using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Trc.Core.Common;

/// <summary>
/// Експорт довільного списку об'єктів у CSV-файл, який коректно відкривається в Excel
/// (роздільник «;», кодування UTF-8 з BOM, заголовки з атрибута [DisplayName]).
/// </summary>
public static class CsvExporter
{
    public static void Export<T>(IEnumerable<T> rows, string path) =>
        Export(rows, typeof(T), path);

    public static void Export(System.Collections.IEnumerable rows, Type itemType, string path)
    {
        var properties = TypeDescriptor.GetProperties(itemType)
            .Cast<PropertyDescriptor>()
            .Where(p => p.IsBrowsable)
            .ToList();

        var sb = new StringBuilder();
        sb.AppendLine(string.Join(";", properties.Select(p => Escape(p.DisplayName))));
        foreach (var row in rows.Cast<object>())
        {
            sb.AppendLine(string.Join(";", properties.Select(p => Escape(Format(p.GetValue(row))))));
        }

        File.WriteAllText(path, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }

    private static string Format(object? value) => value switch
    {
        null => string.Empty,
        DateTime d => d.TimeOfDay == TimeSpan.Zero ? d.ToString("dd.MM.yyyy") : d.ToString("dd.MM.yyyy HH:mm"),
        decimal m => m.ToString("0.00", CultureInfo.GetCultureInfo("uk-UA")),
        double x => x.ToString("0.##", CultureInfo.GetCultureInfo("uk-UA")),
        bool b => b ? "так" : "ні",
        _ => value.ToString() ?? string.Empty
    };

    private static string Escape(string value) =>
        value.IndexOfAny(new[] { ';', '"', '\n', '\r' }) >= 0
            ? "\"" + value.Replace("\"", "\"\"") + "\""
            : value;
}
