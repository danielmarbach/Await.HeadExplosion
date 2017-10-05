using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(0)]
    public class ParallelFor : IRunnable
    {
        public Task Run()
        {
            Parallel.For(5, 10, CpuBound.Compute);

            return Task.CompletedTask;
        }
    }
}