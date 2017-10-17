using System;
using System.IO;
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

    public static void Explain(this ConcurrentExecution runnable, TextWriter writer)
    {
        writer.WriteLine(" - 'Task.WhenAll' materializes enumerable");
        writer.WriteLine(" - Tasks are executed concurrently");
        writer.WriteLine(" - WhenAll task is done when all done");
    }    
}