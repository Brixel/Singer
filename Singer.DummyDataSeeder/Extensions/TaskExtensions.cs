using System;
using System.Threading;
using System.Threading.Tasks;
using WSharp.Extensions;

namespace Singer.DummyDataSeeder.Extensions
{
    internal static class TaskExtensions
    {
        public static async Task WaitForAsync(this Func<Task> action, string message = "")
        {
            var cts = new CancellationTokenSource();
            try
            {
                var task = action();
                Console.WriteLine(message);
                _ = WriteDotsAsync(cts.Token);
                await task;
                cts.Cancel();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                cts.Cancel();
                Console.WriteLine();
                Console.WriteLine($"Error: {e.SerializeJson()}");
            }
        }
        public static async Task<T> WaitForAsync<T>(this Func<Task<T>> action, string message = "")
        {
            T result = default;
            await new Func<Task>(async () => result = await action()).WaitForAsync(message);
            return result;
        }

        public static async Task WriteDotsAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write(".");
                await Task.Delay(1000);
            }
        }
    }
}
