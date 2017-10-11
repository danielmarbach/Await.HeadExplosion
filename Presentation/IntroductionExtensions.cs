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

        public static void Explain(this Introduction runnable)       
        {
            Console.WriteLine(" # Here I will repeat important information that you should not miss.");
            Console.WriteLine(" # Classical Bullet Points which we love and hate.");
        }
    }
}