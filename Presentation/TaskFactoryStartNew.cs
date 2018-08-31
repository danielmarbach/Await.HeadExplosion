using System.Threading;
using System.Threading.Tasks;

[Order(4)]
public class TaskFactoryStartNew : IRunnable
{
    public Task Run()
    {
        return Task.Factory.StartNew(() => CpuBound.Compute(10), CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }
}
