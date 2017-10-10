using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(9)]
    public class CancelTaskOperation : IRunnable
    {
        public Task Run()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(3));
            var token = tokenSource.Token;

            return Task.Delay(TimeSpan.FromMinutes(1), token);
        }
    }
}