using System.Threading.Tasks;

[Order(0)]
class ParallelFor : IRunnable
{
    public Task Run()
    {
        var options = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 4,
        };
        Parallel.For(5, 10, options, CpuBound.Compute);

        return Task.CompletedTask;
    }
}
