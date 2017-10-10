using System.Threading;
using System.Threading.Tasks;

namespace Presentation 
{
    [Order(8)]
    public class CancelTask : IRunnable
    {
        public Task Run()
        {
            var token = new CancellationToken(true);           
            var cancelledTask = Task.Run(() => { }, token);
            return cancelledTask;
        }
    }
}