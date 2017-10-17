using System;

static class TaskRunExtensions
{
    public static void Explain(this TaskRun runnable)
    {
        Console.WriteLine(" # Schedules on the worker thread pool");
        Console.WriteLine(" # Prefered over Task.Factory.StartNew");
    }
}