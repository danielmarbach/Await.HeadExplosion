
using System;
using System.Threading.Tasks;

[Order(5)]
class AsyncAllTheWay : IRunnable
{
    public Task Run()
    {
        return this.WrapInContext(() =>
            Method()
        );
    }

    void Method()
    {
        if(!MethodAsync().Wait(1000))
        {
            throw new TimeoutException("Timed out after deadlock.");
        }
    }

    async Task MethodAsync()
    {
        await Task.Delay(200);
    }
}