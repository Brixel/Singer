using System;
using System.Reflection;

namespace Singer.Helpers.Extensions
{
   public static class MemberInfoExtensions
   {
      public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
         => member.GetCustomAttribute<T>() != null;
   }
}
