using System;
using System.Reflection;

namespace Singer.Helpers.Extensions;

public static class PropertyInfoExtensions
{
    public static void SetToDefaultValue(this PropertyInfo property, object parent)
    {
        var value = property.PropertyType.IsValueType
             ? Activator.CreateInstance(property.PropertyType)
             : null;
        property.SetValue(parent, value);
    }
}
