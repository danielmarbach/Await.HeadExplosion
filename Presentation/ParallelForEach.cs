using System;
using System.Linq;
using System.Threading.Tasks;

[Order(1)]
class ParallelForEach : IRunnable
{
    public Task Run()
    {
        Parallel.ForEach(Enumerable.Range(5, 10), CpuBound.Compute);

        return Task.CompletedTask;
    }
}