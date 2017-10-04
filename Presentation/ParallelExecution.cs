using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(6)]
    public class ParallelExecution : IRunnable
    {
        public Task Run()
        {
            var concurrent = Enumerable.Range(0, 4).Select(t => Task.Run(() =>
            {
                Console.WriteLine($"start {t} / {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1500);
                Console.WriteLine($"done {t} / {Thread.CurrentThread.ManagedThreadId}");
            }));

            return Task.WhenAll(concurrent);
        }
    }
}