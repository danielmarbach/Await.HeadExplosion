using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(1)]
    public class ParallelForEach : IRunnable
    {
        public Task Run()
        {
            Parallel.ForEach(Enumerable.Range(5, 10), CpuBound.Compute);

            return Task.CompletedTask;
        }
    }
}