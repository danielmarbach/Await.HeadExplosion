using System.IO;

static class TaskRunExtensions
{
    public static void Explain(this TaskRun runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Useful when blocking and compute bound operations should be offloaded to the worker thread pool
- One thread from the pool per `Task.Run` when no async body is used
- Prefered over `Task.Factory.StartNew` because it applies reasonable defaults for 99% of the use cases
");
    }
}