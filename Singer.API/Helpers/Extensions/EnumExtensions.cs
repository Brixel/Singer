using System;
using System.Linq;

namespace Singer.Helpers.Extensions
{
   public static class EnumExtensions
   {
      public static string GetDisplayName<T>(this T enumValue) where T : Enum
      {
         var type = typeof(T);
         return type
             .GetMember(enumValue.ToString())
             .FirstOrDefault(x => x.DeclaringType == type)
             ?.GetDisplayName();
      }
   }
}
