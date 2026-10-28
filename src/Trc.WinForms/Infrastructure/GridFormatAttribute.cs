namespace Trc.WinForms.Infrastructure;

/// <summary>Формат відображення значення у стовпці таблиці (наприклад, «dd.MM.yyyy HH:mm»).</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class GridFormatAttribute : Attribute
{
    public GridFormatAttribute(string format) => Format = format;

    public string Format { get; }
}
