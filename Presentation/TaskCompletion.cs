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
        this.PrintStart();
        simulator.Start();
        await taskCompletionSource.Task.ConfigureAwait(false);
        this.PrintEnd();
    }
}