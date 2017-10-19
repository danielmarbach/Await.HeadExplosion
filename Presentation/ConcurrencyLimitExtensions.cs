using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static class ConcurrencyLimitExtensions
{
    public static async Task SimulateWorkThatTakesOneSecond(this ConcurrencyLimit runnable, int workCount, CancellationToken cancellationToken = default(CancellationToken))
    {
        Console.WriteLine($"start {workCount}");
        try
        {
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine($"done {workCount}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"canceled {workCount}");
        }
    }

    public static CancellationToken TokenThatCancelsAfterTwoSeconds(this ConcurrencyLimit runnable) 
    {
        var tokenSource = new CancellationTokenSource();
        tokenSource.CancelAfter(TimeSpan.FromSeconds(2));
        var token = tokenSource.Token;
        return token;
    }

    public static void Explain(this ConcurrencyLimit runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- `SemaphoreSlim` is a handy structure to limit concurrency
- `SemaphoreSlim` does not preserve order
- `SemaphoreSlim` can be used as async lock structure if required (caveat at least 10 times slower than lock)

 |                   Method | Overhead |
 |------------------------- |---------:|
 |          **lock**        |  20 ns   | 
 |       SemaphoreSlim      |  200 ns  |
 |         Semaphore        |  1000 ns |

http://www.albahari.com/threading/part2.aspx
");
    }
}