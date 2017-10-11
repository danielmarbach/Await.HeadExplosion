using System;
using System.Threading;

namespace Presentation
{
    public static class AsyncWithTaskFactoryStartNewExtensions 
    {
        public static void PrintStart(this AsyncWithTaskFactoryStartNew runnable) 
        {
            Console.WriteLine($"start {Thread.CurrentThread.ManagedThreadId} - IsBackground: '{Thread.CurrentThread.IsBackground}' IsThreadPoolThread: '{Thread.CurrentThread.IsThreadPoolThread}'");
        }

        public static void PrintEnd(this AsyncWithTaskFactoryStartNew runnable) 
        {
            Console.WriteLine($"done {Thread.CurrentThread.ManagedThreadId} - IsBackground: '{Thread.CurrentThread.IsBackground}' IsThreadPoolThread: '{Thread.CurrentThread.IsThreadPoolThread}'");
        }        
    }
}