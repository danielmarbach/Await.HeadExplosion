using System.Threading;
using System.Threading.Tasks;

[Order(12)]
class CancelTask : IRunnable
{
    public Task Run()
    {      
        return Task.Run(() => { }, new CancellationToken(true));
    }
}