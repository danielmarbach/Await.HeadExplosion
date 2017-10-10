using System;

namespace Presentation
{
    public static class IntroductionExtensions 
    {
        public static void PrintStart(this Introduction runnable) 
        {
            Console.WriteLine("Start");
        }

        public static void PrintEnd(this Introduction runnable) 
        {
            Console.WriteLine("End");
        }        
    }
}