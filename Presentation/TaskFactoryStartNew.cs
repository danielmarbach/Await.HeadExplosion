using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(3)]
    public class TaskFactoryStartNew : IRunnable
    {
        public Task Run()
        {
            Console.WriteLine("await Task.Factory.StartNew(() => Compute(10))");

            return Task.Factory.StartNew(() => CpuBound.Compute(10), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }
    }
}