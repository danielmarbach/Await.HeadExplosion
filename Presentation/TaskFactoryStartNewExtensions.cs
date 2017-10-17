using System;

static class TaskFactoryStartNewExtensions
{
    public static void Explain(this TaskFactoryStartNew runnable)
    {
        Console.WriteLine(" # Schedules on the worker thread pool");
        Console.WriteLine(" # Shoot yourself in the foot API");
    }
}