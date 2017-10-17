using System;
using System.Threading;

static class ParallelExecutionExtensions
{
    public static void PrintStart(this ParallelExecution runnable, int element)
    {
        Console.WriteLine($"start {element} / {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void PrintEnd(this ParallelExecution runnable, int element)
    {
        Console.WriteLine($"done {element} / {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void Explain(this ParallelExecution runnable)
    {
        Console.WriteLine(" # Nature of Task API allows to combine concurrency and explicit parallelism.");
        Console.WriteLine(" # Degree of Parallelism = Number of Threads used from worker pool.");
    }
}