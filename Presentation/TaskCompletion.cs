using System;
using System.Threading;
using System.Threading.Tasks;
[Order(15)]
class TaskCompletion : IRunnable
{
    public async Task Run()
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        var simulator = new Simulator();
        simulator.Fired += (sender, args) => taskCompletionSource.TrySetResult(true);
        Console.WriteLine($"Start on {Thread.CurrentThread.ManagedThreadId}");
        simulator.Start();
        await taskCompletionSource.Task.ConfigureAwait(false);
        Console.WriteLine($"Continue on {Thread.CurrentThread.ManagedThreadId}");
    }
}
