using System;
using System.Threading;
using System.Threading.Tasks;

[Order(16)]
public class TaskFactoryStartNewLongRunning : IRunnable
{
    public async Task Run()
    {
        await Task.Factory.StartNew(async () =>
        {
            this.PrintStart();

            await Task.Delay(2000);

            this.PrintEnd();
        }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)
        .Unwrap();

        await Task.Run(async () =>
        {
            this.PrintStart();

            await Task.Delay(2000);

            this.PrintEnd();
        });
    }
}