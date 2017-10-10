using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Console.Clear();

            var runnables = (
                from type in typeof(Program).Assembly.GetTypes()
                where typeof(IRunnable).IsAssignableFrom(type) && type != typeof(IRunnable)
                let activatedRunnable = (IRunnable) Activator.CreateInstance(type)
                let order = type.GetCustomAttribute<OrderAttribute>().Order
                orderby order
                select new { Order = order, ActivatedRunnable = activatedRunnable }
            ).ToDictionary(k => k.Order, v => v.ActivatedRunnable);

            PrintRunnables(runnables);

            string line;
            while ((line = Console.ReadLine().ToLowerInvariant()) != "exit")
            {
                if (line == "clear")
                {
                    Console.Clear();
                    PrintRunnables(runnables);
                }

                if(int.TryParse(line, out var itemNumber)) {
                    if (runnables.TryGetValue(itemNumber, out var runnable))
                    {
                        Console.Clear();
                        var currentColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Gray;

                        var stopWatch = Stopwatch.StartNew();
                        try
                        {
                            await runnable.Run().ConfigureAwait(false);
                        }
                        finally
                        {
                            stopWatch.Stop();
                            Console.WriteLine($"execution took {stopWatch.Elapsed.ToString()}");
                            Console.ForegroundColor = currentColor;
                        }
                    }
                }
            }
        }

        private static void PrintRunnables(Dictionary<int, IRunnable> runnables)
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            if(threadIds.Count > 4) {
                threadIds.Clear();
            }

            threadIds.Push(currentThreadId);

            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|================================================|");
            Console.WriteLine($"{$"| Thread(s): {string.Join(",", threadIds)}".PadRight(49)}|");
            Console.WriteLine("|================================================|");
            Console.WriteLine();
            foreach (var kvp in runnables)
            {
                Console.WriteLine($" ({PadBoth(kvp.Key.ToString(), 5)}) {kvp.Value.GetType().Name}");
            }
            Console.WriteLine($" ({PadBoth((runnables.Count - 1).ToString(), 5)}) Clear");
            Console.WriteLine($" ({PadBoth((runnables.Count).ToString(), 5)}) Exit");
            Console.ForegroundColor = currentColor;
        }

        static string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces/2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);
        }

        static Stack<int> threadIds = new Stack<int>(7);
    }
}
