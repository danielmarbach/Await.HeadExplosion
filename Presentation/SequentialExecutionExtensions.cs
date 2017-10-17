using System;
using System.Threading;

static class SequentialExecutionExtensions
{
    public static void PrintStart(this SequentialExecution runnable, int element)
    {
        Console.WriteLine($"start {element} / {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void PrintEnd(this SequentialExecution runnable, int element)
    {
        Console.WriteLine($"done {element} / {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void Explain(this SequentialExecution runnable)
    {
        Console.WriteLine(" # Lazy nature of enumerable creates tasks when iterating");
        Console.WriteLine(" # 'Await' means sequentialize here");
    }
}