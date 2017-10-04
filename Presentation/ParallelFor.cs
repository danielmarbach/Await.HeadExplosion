using System;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(0)]
    public class ParallelFor : IRunnable
    {
        public Task Run()
        {
            Console.WriteLine("Parallel.For(10, 20, Compute):");

            Parallel.For(10, 20, CpuBound.Compute);

            return Task.CompletedTask;
        }
    }
}