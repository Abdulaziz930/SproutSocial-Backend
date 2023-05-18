using System.ComponentModel.DataAnnotations;

namespace SproutSocial.Application.Helpers;

public static class EnumHelper
{
    public static string GetEnumDisplayName<T>(T enumValue)
    {
        var displayAttribute = typeof(T)
            .GetField(enumValue.ToString())
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .SingleOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? enumValue.ToString();
    }
}