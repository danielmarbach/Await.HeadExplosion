using System;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(2)]
    public class TaskRun : IRunnable
    {
        public Task Run()
        {
            return Task.Run(() => CpuBound.Compute(10));
        }
    }
}