using System;
using System.Threading;

namespace Presentation
{

    public static class ConcurrentExecutionExtensions
    {
        public static void PrintStart(this ConcurrentExecution runnable, int element)
        {
            Console.WriteLine($"start {element} / {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void PrintEnd(this ConcurrentExecution runnable, int element)
        {
            Console.WriteLine($"done {element} / {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}