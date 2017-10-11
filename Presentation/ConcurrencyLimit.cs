using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(13)]
    public class ConcurrencyLimit : IRunnable
    {
        public async Task Run()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(2));
            var token = tokenSource.Token;
            const int maxConcurrency = 5;

            var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
            var pumpTask = Task.Run(async () =>
            {
                int workCount = 0;
                while (!token.IsCancellationRequested)
                {
                    await semaphore.WaitAsync(token);

                    var runningTask = Work(workCount++);

                    runningTask.ContinueWith((t, state) =>
                    {
                        var s = (SemaphoreSlim)state;
                        s.Release();
                    }, semaphore, TaskContinuationOptions.ExecuteSynchronously)
                    .Ignore();
                }
            });

            await pumpTask.IgnoreCancellation();

            while (semaphore.CurrentCount != maxConcurrency)
            {
                await Task.Delay(50);
            }
        }

        static async Task Work(int workCount, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"start {workCount}");
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine($"done {workCount}");
        }
    }
}