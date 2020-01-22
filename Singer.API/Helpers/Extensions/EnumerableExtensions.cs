using System;
using System.Collections.Generic;

namespace Singer.Helpers.Extensions
{
   public static class EnumerableExtensions
   {
      /// <summary>Does an action for each of the items in a collection.</summary>
      /// <typeparam name="T">The type of elements in the collection.</typeparam>
      /// <param name="source">The collection to perform the action on.</param>
      /// <param name="action">The action to perform on all the elements of the collection.</param>
      public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
      {
         foreach (var item in source)
            action(item);
      }
   }
}
