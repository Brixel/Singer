using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Singer.DummyDataSeeder.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> collection, Func<T, Task> action)
        {
            await foreach (var item in collection)
                await action(item);
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> collection, Func<T, Task> action)
        {
            foreach (var item in collection)
                await action(item);
        }
    }
}
