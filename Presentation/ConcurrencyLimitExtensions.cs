using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    public static class ConcurrencyLimitExtensions
    {
        public static async Task SimulateWorkThatTakesOneSecond(this ConcurrencyLimit runnable, int workCount, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"start {workCount}");
            try
            {
                await Task.Delay(1000, cancellationToken);
                Console.WriteLine($"done {workCount}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"canceled {workCount}");
            }
        }

        public static CancellationToken TokenThatCancelsAfterTwoSeconds(this ConcurrencyLimit runnable) 
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(2));
            var token = tokenSource.Token;
            return token;
        }
    }
}