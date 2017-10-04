using System;
using System.Threading;

namespace Presentation
{
    public class CpuBound
    {
        public static void Compute(int numberOfElements)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var elements = new int[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
            {
                elements[i] = random.Next(0, 100);
            }

            Console.WriteLine($"Unsorted {numberOfElements}/{threadId}: '{string.Join(",", elements)}'");

            Quicksort(elements, 0, elements.Length - 1);
            
            Console.WriteLine($"Sorted {numberOfElements}/{threadId}: '{string.Join(",", elements)}'");
        }

        static void Quicksort(int[] elements, int left, int right)
        {
            int i = left, j = right;
            var pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    var tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }

        static Random random = new Random();
    }
}