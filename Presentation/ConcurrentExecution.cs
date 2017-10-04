using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(5)]
    public class ConcurrentExecution : IRunnable
    {
        public Task Run()
        {
            var concurrent = Enumerable.Range(0, 4).Select(async t =>
            {
                Console.WriteLine($"start {t} / {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1500);
                Console.WriteLine($"done {t} / {Thread.CurrentThread.ManagedThreadId}");
            });

            return Task.WhenAll(concurrent);
        }
    }
}