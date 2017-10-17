using System.IO;

static class TaskFactoryStartNewExtensions
{
    public static void Explain(this TaskFactoryStartNew runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Useful when blocking and compute bound operations should be offloaded to the worker thread pool
- Useful when finer control is required
- One thread from the pool per `Task.Factory.StartNew` when no async body is used
- Uses `TaskScheduler.Current` by default if you don't specify one which can be a scheduler that wraps the SynchronizationContext
- Is the shoot yourself in the foot API if you don't know what you are doing (more examples later)
");
    }
}