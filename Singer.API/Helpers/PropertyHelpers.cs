using System;
using System.Linq.Expressions;

namespace Singer.Helpers
{
   public class PropertyHelpers
   {
      public static Expression<Func<T, object>> GetPropertySelector<T>(string propertyName)
      {
         var arg = Expression.Parameter(typeof(T), "x");
         var property = Expression.Property(arg, propertyName);
         //return the property as object
         var conv = Expression.Convert(property, typeof(object));
         var exp = Expression.Lambda<Func<T, object>>(conv, arg);
         return exp;
      }
   }
}
