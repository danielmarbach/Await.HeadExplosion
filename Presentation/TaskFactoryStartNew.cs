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
            return Task.Factory.StartNew(() => CpuBound.Compute(10), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }
    }
}