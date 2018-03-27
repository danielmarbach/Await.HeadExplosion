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

 |  Method |      Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |----------:|----------:|----------:|-------:|----------:|
 |  Return | 15.576 ms | 0.4185 ms | 0.0236 ms |   1.00 |     528 B |
 |  Simple | 15.568 ms | 0.8275 ms | 0.0468 ms |   1.00 |     744 B |
 | Actions |  2.008 ms | 0.0756 ms | 0.0043 ms |   0.13 |     560 B |

- NET Core 2.1 preview:

 |  Method |      Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |----------:|----------:|----------:|-------:|----------:|
 |  Return | 15.542 ms | 1.3313 ms | 0.0752 ms |   1.00 |     376 B |
 |  Simple | 15.538 ms | 1.4433 ms | 0.0815 ms |   1.00 |     488 B |
 | Actions |  1.939 ms | 0.4590 ms | 0.0259 ms |   0.12 |     350 B |

");
    }
}