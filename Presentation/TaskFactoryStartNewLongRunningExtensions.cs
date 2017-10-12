using System;
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
}