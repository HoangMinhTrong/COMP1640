using System.Runtime.Serialization;

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
}