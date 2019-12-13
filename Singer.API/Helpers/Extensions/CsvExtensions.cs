using Singer.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Singer.Helpers.Extensions
{
   public static class CsvExtensions
   {
      /// <summary>
      ///     Serializes a list of items in a csv format. Serialization of properties is done with
      ///     the *.ToString() method.
      /// </summary>
      /// <typeparam name="T">Type of the items to serialize.</typeparam>
      /// <param name="list">List ot serialize.</param>
      /// <param name="includeHeaders">
      ///     Indicates whether the headers should also be printed out.
      /// </param>
      /// <param name="delimiter">The delimiter to place between properties.</param>
      /// <returns>The csv representation of the list.</returns>
      public static string SerializeCsv<T>(this IEnumerable<T> list, bool includeHeaders = true, char delimiter = ';')
      {
         if (list == null)
            throw new ArgumentNullException(nameof(list));

         var builder = new StringBuilder();
         var properties = typeof(T)
            .GetProperties()
            .Where(x => x.GetCustomAttribute<CsvIgnoreAttribute>() == null)
            .ToList();

         if (includeHeaders)
         {
            properties.Aggregate(builder, (b, p) =>
            {
               var propertyName = p.GetCustomAttribute<CsvPropertyAttribute>()?.PropertyName ?? p.Name;
               return builder.Append($"{propertyName}{delimiter}");
            });
            builder.AppendLine();
         }

         foreach (var item in list)
         {
            properties.Aggregate(builder, (b, p) => builder.Append($"{p.GetValue(item)}{delimiter}"));
            builder.AppendLine();
         }

         return builder.ToString();
      }
   }
}
