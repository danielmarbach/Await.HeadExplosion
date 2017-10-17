using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

static class ShortcutStatemachineExtensions
{
    public static async Task PrintStackInformation(this ShortcutStatemachine runnable, Func<Task> method)
    {
        try
        {
            await method().ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            var stackTrace = new StackTrace(1, true);

            Console.WriteLine($"{method.Method.Name}: FrameCount {stackTrace.FrameCount} / Has AsyncMethodBuilder '{stackTrace.ToString().Contains("AsyncTaskMethodBuilder")}'");
        }
    }

    public static void Explain(this ShortcutStatemachine runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- For highperf scenario `async` keyword can be omitted
- Apply carefully and only after measuring
- For most scenarios apply the keyword since it prevents mistakes
- NET Core 2.0:

 |  Method |     Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |---------:|----------:|----------:|-------:|----------:|
 |  Return | 15.61 ms | 0.0150 ms | 0.0140 ms |   1.00 |     528 B |
 |  Simple | 15.61 ms | 0.0184 ms | 0.0172 ms |   1.00 |     744 B |
- NET Core 2.1 preview:

 |  Method |     Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |---------:|----------:|----------:|-------:|----------:|
 |  Return | 15.61 ms | 0.0150 ms | 0.0140 ms |   1.00 |     520 B |
 |  Simple | 15.61 ms | 0.0184 ms | 0.0172 ms |   1.00 |     736 B | 

");
    }
}