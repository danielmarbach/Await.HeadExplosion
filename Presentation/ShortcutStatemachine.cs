using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation
{
    [Order(11)]
    public class ShortcutStatemachine : IRunnable
    {
        public async Task Run()
        {
            await this.PrintStackInformation(DoesNotShortcut);

            await this.PrintStackInformation(DoesShortcut);
        }

        private static async Task DoesNotShortcut()
        {
            var tcs = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            await Task.Delay(TimeSpan.FromMinutes(1), tcs.Token);
        }

        private static Task DoesShortcut()
        {
            var tcs = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            return Task.Delay(TimeSpan.FromMinutes(1), tcs.Token);
        }
    }
}