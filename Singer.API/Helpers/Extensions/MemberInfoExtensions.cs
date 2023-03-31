using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Singer.Helpers.Extensions;

public static class MemberInfoExtensions
{
    public static string GetDisplayName(this MemberInfo member)
    {
        return member
           .GetCustomAttribute<DisplayAttribute>()
           ?.GetName()
           ?? member.GetCustomAttribute<DisplayNameAttribute>()
           ?.DisplayName
           ?? member.Name;
    }

    public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
       => member.GetCustomAttribute<T>() != null;
}
