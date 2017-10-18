using System;
using System.IO;
using System.Threading;

static class SimpleAsyncExtensions
{
    public static void PrintStart(this SimpleAsync runnable)
    {
        Console.WriteLine($"start {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void PrintEnd(this SimpleAsync runnable)
    {
        Console.WriteLine($"done {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void Explain(this SimpleAsync runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Every `await` statement is a chance for the calling thread to do something else
- Much more efficient due to less thread usage
- Can achieve higher saturation of ressources available
");
    }
}
