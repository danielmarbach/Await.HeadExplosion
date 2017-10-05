using System;
using System.Threading;

namespace Presentation
{

    public static class SequentialExecutionExtensions
    {
        public static void PrintStart(this SequentialExecution runnable, int element)
        {
            Console.WriteLine($"start {element} / {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void PrintEnd(this SequentialExecution runnable, int element)
        {
            Console.WriteLine($"done {element} / {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}