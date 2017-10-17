using System.Threading.Tasks;

[Order(0)]
class ParallelFor : IRunnable
{
    public Task Run()
    {
        Parallel.For(5, 10, CpuBound.Compute);

        return Task.CompletedTask;
    }
}
