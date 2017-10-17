using System;
using System.IO;
using System.Threading;

static class TaskFactoryStartNewLongRunningExtensions 
{
    public static void PrintStart(this TaskFactoryStartNewLongRunning runnable) 
    {
        Console.WriteLine($"start {Thread.CurrentThread.ManagedThreadId} - IsBackground: '{Thread.CurrentThread.IsBackground}' IsThreadPoolThread: '{Thread.CurrentThread.IsThreadPoolThread}'");
    }

    public static void PrintEnd(this TaskFactoryStartNewLongRunning runnable) 
    {
        Console.WriteLine($"done {Thread.CurrentThread.ManagedThreadId} - IsBackground: '{Thread.CurrentThread.IsBackground}' IsThreadPoolThread: '{Thread.CurrentThread.IsThreadPoolThread}'");
    } 

    public static void Explain(this TaskFactoryStartNewLongRunning runnable, TextWriter writer) 
    {
        writer.WriteLine(" - 'LongRunning' flag is a waste in combination with an async body");
        writer.WriteLine(" - Don't try to be smarter than the TPL ;)");
    }               
}