using System;
using System.Linq;
using System.Threading.Tasks;

[Order(2)]
class ParallelForEach : IRunnable
{
    public Task Run()
    {
        var options = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 4,
        };

        Parallel.ForEach(Enumerable.Range(5, 10), options, CpuBound.Compute);

        return Task.CompletedTask;
    }
}