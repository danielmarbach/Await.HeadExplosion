using System;
using System.Threading;

namespace Presentation
{
    public static class IntroductionExtensions 
    {
        public static void PrintStart(this Introduction runnable) 
        {
            Console.WriteLine($"start {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void PrintEnd(this Introduction runnable) 
        {
            Console.WriteLine($"end {Thread.CurrentThread.ManagedThreadId}");
        }        
    }
}