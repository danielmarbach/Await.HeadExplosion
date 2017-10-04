using System;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(2)]
    public class TaskRun : IRunnable
    {
        public Task Run()
        {
            Console.WriteLine("await Task.Run(() => Compute(10)):");

            return Task.Run(() => CpuBound.Compute(10));
        }
    }
}