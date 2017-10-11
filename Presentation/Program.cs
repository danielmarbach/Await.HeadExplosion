using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    class Program
    {
        class RunnerWithExplainer 
        {
            public RunnerWithExplainer(IRunnable runnable, Action explainer) 
            {
                this.runnable = runnable;
                this.explainer = explainer;
                this.Name = runnable.GetType().Name;
            }

            public string Name { get; private set; }

            public Task Run() 
            {
                return runnable.Run();
            }

            public void Explain() 
            {
                explainer();
            }

            IRunnable runnable;
            Action explainer;
        }

        static async Task Main(string[] args)
        {
            Console.Clear();

            var runnables = (
                from type in typeof(Program).Assembly.GetTypes()
                where typeof(IRunnable).IsAssignableFrom(type) && type != typeof(IRunnable)
                let activatedRunnable = (IRunnable) Activator.CreateInstance(type)
                let order = type.GetCustomAttribute<OrderAttribute>().Order
                let explainer = CreateExplainer(activatedRunnable)
                orderby order
                select new { Order = order, ActivatedRunnable = activatedRunnable, Explainer = explainer }
            ).ToDictionary(k => k.Order, v => new RunnerWithExplainer(v.ActivatedRunnable, v.Explainer));

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
                        var run = runnable.Run();
                        try
                        {
                            Console.WriteLine($"-- Task state: {run.Status.ToString()}");
                            await run.ConfigureAwait(false);
                        }
                        catch(Exception ex) 
                        {
                            Console.WriteLine($"-- Caught: {ex.GetType().Name}('{ex.Message}')");
                        }
                        finally
                        {
                            stopWatch.Stop();
                            Console.WriteLine($"-- Task state: {run.Status.ToString()}");
                            Console.WriteLine($"-- Execution time: {stopWatch.Elapsed.ToString()}");

                            runnable.Explain();

                            Console.ForegroundColor = currentColor;
                        }
                    }
                }
            }
        }

        static Action CreateExplainer(IRunnable runnable) 
        {
            var extensionType = Type.GetType($"Presentation.{runnable.GetType().Name}Extensions", false);
            if(extensionType != null) 
            {
                var method = extensionType.GetMethod("Explain", BindingFlags.Public | BindingFlags.Static);
                if(method != null) 
                {
                    var block = Expression.Block(Expression.Call(null, ExplanationHeaderPrinter), Expression.Call(null, method, Expression.Constant(runnable)));
                    return Expression.Lambda<Action>(block).Compile();;
                }
            }
            return () => { };
        }

        static void PrintExplanationHeader() 
        {
            Console.WriteLine();
            Console.WriteLine("|================================================|");
            Console.WriteLine($"| {"Remember".PadRight(47)}|");
            Console.WriteLine("|================================================|");
            Console.WriteLine();
        }

        static void PrintRunnables(Dictionary<int, RunnerWithExplainer> runnables)
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
                Console.WriteLine($" ({PadBoth(kvp.Key.ToString(), 5)}) {kvp.Value.Name}");
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
        static MethodInfo ExplanationHeaderPrinter = typeof(Program).GetMethod(nameof(PrintExplanationHeader), BindingFlags.NonPublic | BindingFlags.Static);
    }
}
