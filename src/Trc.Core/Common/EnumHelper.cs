using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Trc.Core.Common;

/// <summary>Елемент списку для прив'язки переліку до ComboBox.</summary>
public sealed record EnumOption<T>(T Value, string Name) where T : struct, Enum
{
    public override string ToString() => Name;
}

public static class EnumHelper
{
    /// <summary>Повертає українську назву значення переліку з атрибута [Display].</summary>
    public static string GetDisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? value.ToString();
    }

    public static List<EnumOption<T>> GetOptions<T>() where T : struct, Enum =>
        Enum.GetValues<T>().Select(v => new EnumOption<T>(v, v.GetDisplayName())).ToList();
}
