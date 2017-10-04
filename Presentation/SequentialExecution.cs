using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(4)]
    public class SequentialExecution : IRunnable
    {
        public async Task Run()
        {
            var sequential = Enumerable.Range(0, 4).Select(async t =>
            {
                Console.WriteLine($"start {t} / {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1500);
                Console.WriteLine($"done {t} / {Thread.CurrentThread.ManagedThreadId}");
            });

            foreach (var task in sequential)
            {
                await task;
            }
        }

    }
}