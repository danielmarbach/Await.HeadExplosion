using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[Order(21)]
class CustomAwaiter : IRunnable
{
    public async Task Run()
    {
        await 1000;
        await TimeSpan.FromSeconds(1);

    }
}

public static class CustomTaskExtensions
{

    public static TaskAwaiter GetAwaiter(this int millisecondsDelay)
    {
        return Task.Delay(millisecondsDelay).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this TimeSpan delay)
    {
        return Task.Delay(delay).GetAwaiter();
    }
}