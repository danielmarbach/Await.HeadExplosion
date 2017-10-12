using System.Threading;
using System.Threading.Tasks;

[Order(13)]
class ConcurrencyLimit : IRunnable
{
    public async Task Run()
    {
        var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        var pumpTask = Task.Run(async () =>
        {
            var token = this.TokenThatCancelsAfterTwoSeconds();
            int workCount = 0;
            while (!token.IsCancellationRequested)
            {
                await semaphore.WaitAsync(token);

                var runningTask = this.SimulateWorkThatTakesOneSecond(workCount++);

                runningTask.ContinueWith((t, state) =>
                {
                    var s = (SemaphoreSlim)state;
                    s.Release();
                }, semaphore, TaskContinuationOptions.ExecuteSynchronously)
                .Ignore();
            }
        });
        
        await pumpTask.IgnoreCancellation();
        
        await WaitForPendingWork(semaphore);
    }

    async Task WaitForPendingWork(SemaphoreSlim semaphore) 
    {
        while (semaphore.CurrentCount != maxConcurrency)
        {
            await Task.Delay(50);
        }
    }

    const int maxConcurrency = 5;
}
