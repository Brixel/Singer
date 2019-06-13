using System;
using System.Linq;
using System.Linq.Expressions;

namespace Singer.Helpers.Extensions
{
   public static class IQueryableExtensions
   {
      public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
      {
         return source.OrderBy(ToLambda<T>(propertyName));
      }
      public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
      {
         return source.ThenBy(ToLambda<T>(propertyName));
      }

      public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
      {
         return source.OrderByDescending(ToLambda<T>(propertyName));
      }

      private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
      {
         var parameter = Expression.Parameter(typeof(T));
         var property = Expression.Property(parameter, propertyName);
         var propAsObject = Expression.Convert(property, typeof(object));

         return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
      }

      public static IQueryable<T> TakePage<T>(this IOrderedQueryable<T> orderedQueryable, int pageIndex, int pageSize)
      {
         return orderedQueryable
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
      }
   }
}
