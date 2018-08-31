using System.Threading.Tasks;

[Order(3)]
public class TaskRun : IRunnable
{
    public Task Run()
    {
        return Task.Run(() => CpuBound.Compute(10));
    }
}