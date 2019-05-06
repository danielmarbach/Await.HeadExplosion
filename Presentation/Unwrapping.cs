using System;
using System.Threading.Tasks;

[Order(12)]
public class Unwrapping : IRunnable
{
    public Task Run()
    {
        var proxyTask = Task.Factory.StartNew(async () =>
        {
            this.PrintStartProxy(1);
            await Task.Delay(TimeSpan.FromSeconds(5));
            this.PrintEndActual(1);
        }).ContinueWith(t => this.PrintEndProxy(1));

        var actualTask = Task.Factory.StartNew(async () =>
        {
            this.PrintStartActual(2);
            await Task.Delay(TimeSpan.FromSeconds(2));
            this.PrintEndActual(2);
        }).Unwrap();

        return Task.WhenAll(proxyTask, actualTask);
    }
}