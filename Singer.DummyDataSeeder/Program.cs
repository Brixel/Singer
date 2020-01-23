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

                _ = RunAsync(x);

                while (true)
                    _ = Console.ReadLine();
            });

        private static bool EnsureWork(ref Options options)
        {
            if (options.GenerateNothing)
            {
                options.GenerateAdmins = AskBool("Generate admins?");
                options.GenerateLegalGuardians = AskBool("Generate legal guardians?");
                options.GenerateCareUsers = AskBool("Generate care users?");
                options.GenerateEvents = AskBool("Generate events?");
                options.LinkCareUsersAndLegalGuardians = AskBool("Link care users and legal guardians?");
            }

            return !options.GenerateNothing;
        }

        private static async Task RunAsync(Options options)
        {
            var creator = new DataCreator("https://localhost:5001");

            if (options.GenerateAdmins)
            {
                Console.WriteLine("Creating admins...");
                await creator.CreateAdminsAsync();
            }

            if (options.GenerateLegalGuardians)
            {
                Console.WriteLine("Creating legal guardians...");
                await  creator.CreateLegalGuardiansAsync();
            }

            if (options.GenerateCareUsers)
            {
                Console.WriteLine("Creating care users...");
                await creator.CreateCareUsersAsync();
            }

            if (options.GenerateEvents)
            {
                Console.WriteLine("Creating events...");
                await creator.CreateEventsAsync();
            }

            if (options.LinkCareUsersAndLegalGuardians)
            {
                Console.WriteLine("Linking care users to legal guardians...");
                await creator.LinkCareUsersAndLegalGuardiansAsync();
            }
        }

        private static bool AskBool(string question)
        {
            Console.Write($"{question} [Y(Yes)/n(no)]");
            var line = Console.ReadLine().ToLower();
            return string.IsNullOrWhiteSpace(line) || line == "yes" || line == "y";
        }
    }
}
