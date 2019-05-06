using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

[Order(9)]
class SequentialExecution : IRunnable
{
    public async Task Run()
    {
        var sequential = Enumerable.Range(0, 4).Select(async t =>
        {
            this.PrintStart(t);

            await Task.Delay(1500);

            this.PrintEnd(t);
            
        });

        foreach (var task in sequential)
        {
            await task;
        }
    }
}