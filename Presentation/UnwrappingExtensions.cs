using System;

namespace Presentation
{
    public static class UnwrappingExtensions 
    {
        public static void PrintStartProxy(this Unwrapping runnable, int id) 
        {
            Console.WriteLine($"start proxy {id}");
        }

        public static void PrintEndProxy(this Unwrapping runnable, int id) 
        {
            Console.WriteLine($"end proxy {id}");
        }

        public static void PrintStartActual(this Unwrapping runnable, int id) 
        {
            Console.WriteLine($"start actual {id}");
        }  

        public static void PrintEndActual(this Unwrapping runnable, int id) 
        {
            Console.WriteLine($"end actual {id}");
        }        
    }
}