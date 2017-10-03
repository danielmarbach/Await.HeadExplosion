using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

[Config(typeof(Config))]
public class TaskImprovements
{
    [Benchmark(Baseline = true)]
    public async Task Return()
    {
        await DoWorkReturn();
    }

    [Benchmark]
    public async Task Simple()
    {
        await DoWork();
    }

    [Benchmark]
    public async Task Actions()
    {
        await SomeMethod();
    }

    static Task DoWorkReturn()
    {
        return Task.Delay(1);
    }

    static async Task DoWork()
    {
        await Task.Delay(1);
    }

    static async Task SomeMethod()
    {
        await Task.Run(() => Thread.Sleep(1));
    }
}