using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var runnables = (
                from type in typeof(Program).Assembly.GetTypes()
                where typeof(IRunnable).IsAssignableFrom(type) && type != typeof(IRunnable)
                let activatedRunnable = (IRunnable) Activator.CreateInstance(type)
                let order = type.GetCustomAttribute<OrderAttribute>().Order
                orderby order
                select activatedRunnable
            ).ToDictionary(k => k.GetType().Name.ToLowerInvariant(), v => v);

            PrintRunnables(runnables);

            string line;
            while ((line = Console.ReadLine().ToLowerInvariant()) != "exit")
            {
                if (line == "clear")
                {
                    Console.Clear();
                    PrintRunnables(runnables);
                }

                if (runnables.TryGetValue(line, out var runnable))
                {
                    await runnable.Run()
                        .ConfigureAwait(false);
                }
            }
        }

        private static void PrintRunnables(Dictionary<string, IRunnable> runnables)
        {
            Console.WriteLine("|================================================|");
            Console.WriteLine($"{$"| Thread: {Thread.CurrentThread.ManagedThreadId}".PadRight(49)}|");
            Console.WriteLine("|================================================|");

            Console.WriteLine("Select one of the options:");
            Console.WriteLine();
            foreach (var runnable in runnables)
            {
                Console.WriteLine($"- {runnable.Key}");
            }
            Console.WriteLine("- clear");
            Console.WriteLine("- exit");
        }
    }
}
