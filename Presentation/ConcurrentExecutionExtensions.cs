using System;
using System.Threading;

static class ConcurrentExecutionExtensions
{
    public static void PrintStart(this ConcurrentExecution runnable, int element)
    {
        Console.WriteLine($"start {element} / {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void PrintEnd(this ConcurrentExecution runnable, int element)
    {
        Console.WriteLine($"done {element} / {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void Explain(this ConcurrentExecution runnable)
    {
        Console.WriteLine(" # 'Task.WhenAll' materializes enumerable");
        Console.WriteLine(" # Tasks are executed concurrently");
        Console.WriteLine(" # WhenAll task is done when all done");
    }    
}