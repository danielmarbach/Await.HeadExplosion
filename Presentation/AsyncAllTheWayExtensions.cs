
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static class AsyncAllTheWayExtensions
{
    static LimitedConcurrencyLevelTaskScheduler scheduler = new LimitedConcurrencyLevelTaskScheduler(1);

    public static Task WrapInContext(this AsyncAllTheWay runnable, Action action)
    {
        return Task.Factory.StartNew(() =>
        {
            action();
        }, CancellationToken.None, TaskCreationOptions.None, scheduler);
    }

    public static void Explain(this AsyncAllTheWay runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- It is OK to call sync code from async context
- It defeats the purpose of async when async is called from sync code
- It is dangerous to call async code, it can lead to deadlocks.
");
    }
}