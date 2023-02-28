using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Utilities.Helpers;

public static class EnumMemberAttributeHelper
{
    public static string GetEnumMemberValue(Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);

        var attribute = type.GetField(name)
            .GetCustomAttributes(typeof(EnumMemberAttribute), false)
            .SingleOrDefault() as EnumMemberAttribute;

        return attribute?.Value ?? name;
    }
    
    public static IEnumerable<SelectListItem> GetSelectListItems<T>() where T : struct, Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = GetEnumMemberValue(e)
            })
            .ToList();
    }
}