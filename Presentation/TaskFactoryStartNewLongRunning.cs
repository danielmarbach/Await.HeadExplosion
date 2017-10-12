using System;
using System.Threading;
using System.Threading.Tasks;

[Order(12)]
public class TaskFactoryStartNewLongRunning : IRunnable
{
    public async Task Run()
    {
        var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        await Task.Factory.StartNew(async() => {
            this.PrintStart();

            while(!tokenSource.IsCancellationRequested) 
            {
                await Task.Delay(2);
            }

            this.PrintEnd();
        }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)
        .Unwrap();

        tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        await Task.Run(async() => {
            this.PrintStart();

            while(!tokenSource.IsCancellationRequested) 
            {
                await Task.Delay(2);
            }

            this.PrintEnd();
        });
    }
}