using System.Threading.Tasks;

[Order(0)]
class ParallelInvoke : IRunnable
{
    public Task Run()
    {
        Parallel.Invoke(new ParallelOptions{ MaxDegreeOfParallelism = 4}, 
            () => CpuBound.Compute(10), 
            () => CpuBound.Compute(11),
            () => CpuBound.Compute(12),
            () => CpuBound.Compute(13),
            () => CpuBound.Compute(14),
            () => CpuBound.Compute(15)
        );
        return Task.CompletedTask;
    }
}