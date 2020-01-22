using System;
using System.Threading.Tasks;
using CommandLine;

namespace Singer.DummyDataSeeder
{
    internal class Program
    {
        private static void Main(string[] args)
            => _ = Parser.Default
            .ParseArguments<Options>(args)
            .WithParsed(x =>
            {
                if (!EnsureWork(ref x))
                {
                    Console.WriteLine("Nothing to do");
                    return;
                }

                Run(x);
            });

        private static bool EnsureWork(ref Options options)
        {
            if (options.GenerateNothing)
            {
                options.GenerateAdmins = AskBool("Generate admins?");
                options.GenerateAdmins = AskBool("Generate legal guardians?");
                options.GenerateCareUsers = AskBool("Generate care users?");
                options.GenerateEvents = AskBool("Generate events?");
                options.LinkCareUsersAndLegalGuardians = AskBool("Link care users and legal guardians?");
            }

            return !options.GenerateNothing;
        }

        private static void Run(Options options)
        {
            var creator = new DataCreator("https://localhost:5001");

            var task = Task.CompletedTask;

            if (options.GenerateAdmins)
                task = task.ContinueWith(x =>
                {
                    Console.WriteLine("Creating admins...");
                    return creator.CreateAdminsAsync();
                });

            if (options.GenerateLegalGuardians)
                task = task.ContinueWith(x =>
                {
                    Console.WriteLine("Creating legal guardians...");
                    return creator.CreateLegalGuardiansAsync();
                });

            if (options.GenerateCareUsers)
                task = task.ContinueWith(x =>
                {
                    Console.WriteLine("Creating care users...");
                    return creator.CreateCareUsersAsync();
                });

            if (options.GenerateEvents)
                task = task.ContinueWith(x =>
                {
                    Console.WriteLine("Creating events...");
                    return creator.CreateEventsAsync();
                });

            if (options.LinkCareUsersAndLegalGuardians)
                task = task.ContinueWith(x =>
                {
                    Console.WriteLine("Linking care users to legal guardians...");
                    return creator.LinkCareUsersAndLegalGuardiansAsync();
                });
        }

        private static bool AskBool(string question)
        {
            Console.Write($"{question} [Y(Yes)/n(no)]");
            var line = Console.ReadLine().ToLower();
            return string.IsNullOrWhiteSpace(line) || line == "yes" || line == "y";
        }
    }
}
