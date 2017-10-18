
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

[Order(21)]
class ConfigureAwait : IRunnable
{
    public Task Run()
    {
        return this.WrapInContext(async () =>
        {
            this.PrintStart();
            await Method();
            this.PrintEnd();

            this.PrintStart();
            await Method().ConfigureAwait(false);
            this.PrintEnd();
        });
    }

    async Task Method()
    {
        this.PrintBeforeDelay();
        await Task.Delay(100).ConfigureAwait(false);
        this.PrintAfterDelayConfigureAwait();
        await Task.Delay(100);
        this.PrintAfterDelay();
    }
}