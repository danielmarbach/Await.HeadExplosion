using System;
using System.IO;
using System.Threading;

static class TaskFactoryStartNewLongRunningExtensions 
{
    public static void PrintStart(this TaskFactoryStartNewLongRunning runnable) 
    {
        Console.WriteLine($"start {Thread.CurrentThread.ManagedThreadId} IsThreadPoolThread: '{Thread.CurrentThread.IsThreadPoolThread}'");
    }

    public static void PrintEnd(this TaskFactoryStartNewLongRunning runnable) 
    {
        Console.WriteLine($"done {Thread.CurrentThread.ManagedThreadId} IsThreadPoolThread: '{Thread.CurrentThread.IsThreadPoolThread}'");
    } 

    public static void Explain(this TaskFactoryStartNewLongRunning runnable, TextWriter writer) 
    {
                writer.WriteLine(@"
- `TaskCreationOptions.LongRunning` instruct the TPL to create a background thread (non-pooled thread)                
- First `await` statement will return the background (non-pooled) thread, waste is generated
- Useful only for long-running loops without async body
- Don't try to be smarter than the TPL ;)
");
    }               
}