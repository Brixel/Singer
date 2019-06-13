using System;
using System.Linq;
using System.Linq.Expressions;
using Singer.Services;

namespace Singer.Helpers.Extensions
{
   // ReSharper disable once InconsistentNaming
   public static class IQueryableExtensions
   {
      public static IQueryable<T> Filter<T>(this IQueryable<T> source, StringFilter<T> filter)
         => source.Where(filter.GetFilterExpression());

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
