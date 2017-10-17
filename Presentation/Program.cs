using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    class RunnerWithExplainer 
    {
        public RunnerWithExplainer(IRunnable runnable, Action<TextWriter> explainer) 
        {
            this.runnable = runnable;
            this.explainer = explainer;
            Name = runnable.GetType().Name;
        }

        public string Name { get; }

        public Task Run() 
        {
            return runnable.Run();
        }

        public void Explain(TextWriter writer = null) 
        {
            explainer(writer ?? Console.Out);
        }

        readonly IRunnable runnable;
        readonly Action<TextWriter> explainer;
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

        UpdateDescription(runnables);
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
                        Console.WriteLine($"-- Task state: {run.Status}");
                        await run.ConfigureAwait(false);
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine($"-- Caught: {ex.GetType().Name}('{ex.Message}')");
                    }
                    finally
                    {
                        stopWatch.Stop();
                        Console.WriteLine($"-- Task state: {run.Status}");
                        Console.WriteLine($"-- Execution time: {stopWatch.Elapsed}");

                        runnable.Explain();

                        Console.ForegroundColor = currentColor;
                    }
                }
            }
        }
    }

    static Action<TextWriter> CreateExplainer(IRunnable runnable) 
    {
        var extensionType = Type.GetType($"{runnable.GetType().Name}Extensions", false);
        if(extensionType != null) 
        {
            var method = extensionType.GetMethod("Explain", BindingFlags.Public | BindingFlags.Static);
            if(method != null) 
            {
                var parameter = Expression.Parameter(typeof(TextWriter), "writer");
                var block = Expression.Block(Expression.Call(null, ExplanationHeaderPrinter), Expression.Call(null, method, Expression.Constant(runnable), parameter));
                return Expression.Lambda<Action<TextWriter>>(block, parameter).Compile();;
            }
        }
        return writer => { };
    }

    static void PrintExplanationHeader() 
    {
        if (explanationHeaderEnabled)
        {
            Console.WriteLine();
            Console.WriteLine("|================================================|");
            Console.WriteLine($"| {"Remember".PadRight(47)}|");
            Console.WriteLine("|================================================|");
            Console.WriteLine();
        }
    }

    static void PrintRunnables(Dictionary<int, RunnerWithExplainer> runnables)
    {
        var currentThreadId = Thread.CurrentThread.ManagedThreadId;
        if(threadIds.Count > 4) {
            threadIds.Clear();
        }

        threadIds.Push(currentThreadId);

        var longest = runnables.Values.Max(d => d.Name.Length) + 5;
        int fullWidth = longest * 2;

        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        
        Console.WriteLine($"|{string.Join("=", Enumerable.Repeat(string.Empty, fullWidth))}|");
        Console.WriteLine($"{$"| Thread(s): {string.Join(",", threadIds)}".PadRight(fullWidth)}|");
        Console.WriteLine($"|{string.Join("=", Enumerable.Repeat(string.Empty, fullWidth))}|");
        Console.WriteLine();       

        var elements = runnables.Values.Count;
        var half = (elements / 2);
        for (int i = 0; i < half; i++)
        {
            var left = runnables.ElementAtOrDefault(i);
            if(left.Equals(default))
            {
                break;
            }
            var right = runnables.ElementAtOrDefault(i+half);
            if(right.Equals(default))
            {
                break;
            }

            if(left.Key == right.Key) 
            {
                continue;
            }
            var leftString = $" ({PadBoth(left.Key.ToString(), 5)}) {left.Value.Name}";
            var rightString = $"({PadBoth(right.Key.ToString(), 5)}) {right.Value.Name}";
            Console.WriteLine($"{leftString.PadRight(longest)}{rightString}");
        }

        if(elements % 2 == 1) 
        {
            var last = runnables.Last();
            var lastString = $"({PadBoth(last.Key.ToString(), 5)}) {last.Value.Name}";
            Console.WriteLine($"{"".PadRight(longest)}{lastString}");
        }

        Console.ForegroundColor = currentColor;
    }

    static string PadBoth(string source, int length)
    {
        int spaces = length - source.Length;
        int padLeft = spaces/2 + source.Length;
        return source.PadLeft(padLeft).PadRight(length);
    }

    static void UpdateDescription(Dictionary<int, RunnerWithExplainer> runnables)
    {
        explanationHeaderEnabled = false;
        try
        {
            using (var file = File.CreateText("README.MD"))
            {
                foreach (var item in runnables.Values)
                {
                    file.WriteLine($"## {item.Name}");
                    item.Explain(file);
                }
            }
        }
        finally
        {
            explanationHeaderEnabled = true;
        }
    }

    static bool explanationHeaderEnabled = true;
    static Stack<int> threadIds = new Stack<int>(7);
    static MethodInfo ExplanationHeaderPrinter = typeof(Program).GetMethod(nameof(PrintExplanationHeader), BindingFlags.NonPublic | BindingFlags.Static);
}