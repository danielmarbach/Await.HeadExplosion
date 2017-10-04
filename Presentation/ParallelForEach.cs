using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(1)]
    public class ParallelForEach : IRunnable
    {
        public Task Run()
        {
            Console.WriteLine("Parallel.ForEach(Enumerable.Range(10, 20), Compute):");

            Parallel.ForEach(Enumerable.Range(10, 20), CpuBound.Compute);

            return Task.CompletedTask;
        }
    }
}