using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

[Order(19)]
class ThreadLimit : IRunnable
{
    public async Task Run()
    {
        await LimitingThreads(TaskCreationOptions.None, false);
        await LimitingThreads(TaskCreationOptions.None, true );
        await LimitingThreads(TaskCreationOptions.HideScheduler, false );
        await LimitingThreads(TaskCreationOptions.HideScheduler, true );
    }

    Task LimitingThreads(TaskCreationOptions options, bool configureAwait)
    {      
        this.PrintOptions(options, configureAwait);

        var scheduler = new LimitedConcurrencyLevelTaskScheduler(1);
        return this.PumpWithSemaphoreConcurrencyTwo((current, token) => 
        {
            return Task.Factory.StartNew(() =>
            {
                return WorkUnderSpecialScheduler(configureAwait, current);
            }, CancellationToken.None, options, scheduler)
            .Unwrap();
        });
    }
    
    Task WorkUnderSpecialScheduler(bool configureAwait, int current, CancellationToken token = default(CancellationToken))
    {
        var startNewTask = Task.Factory.StartNew(async () =>
        {
            this.PrintStartNewBefore(current);

            await Task.Delay(1000, token).ConfigureAwait(configureAwait);

            this.PrintStartNewAfter(current);
        });

        var runTask = Task.Run(async () =>
        {
            this.PrintRunBefore(current);

            await Task.Delay(1000, token).ConfigureAwait(configureAwait);

            this.PrintRunAfter(current);
        });

        return Task.WhenAll(startNewTask.Unwrap(), runTask);
    }
}