using System;
using System.IO;
using System.Threading;

static class IntroductionExtensions 
{
    public static void PrintStart(this Introduction runnable) 
    {
        Console.WriteLine($"start {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void PrintEnd(this Introduction runnable) 
    {
        Console.WriteLine($"end {Thread.CurrentThread.ManagedThreadId}");
    }  

    public static void Explain(this Introduction runnable, TextWriter writer)       
    {
        writer.WriteLine(@"
- Each sample is contained in a runnable class
- Extension methods are used to hide non important details
- `Thread(s)` view will show the last used thread to render the console also refered to as the `main` thread
");
    }
}