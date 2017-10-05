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
                this.PrintStart(t);

                await Task.Delay(1500);

                this.PrintEnd(t);
            });

            return Task.WhenAll(concurrent);
        }
    }
}