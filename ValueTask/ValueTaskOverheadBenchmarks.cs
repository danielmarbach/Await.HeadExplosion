using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;

// From https://github.com/adamsitnik/StateOfTheDotNetPerformance/blob/master/ValueTaskOverheadBenchmarks.cs
// All credit go to Adam Sitnik
[MemoryDiagnoser]
[ShortRunJob]
public class ValueTaskOverheadBenchmarks
{
    [Params(100, 1000)]
    public int Repeats { get; set; }

    [Benchmark]
    public Task<int> ConsumeTask() => ConsumeTask(Repeats);

    [Benchmark]
    public ValueTask<int> ConsumeValueTaskWrong() => ConsumeWrong(Repeats);

    [Benchmark(Baseline = true)]
    public ValueTask<int> ConsumeValueTaskProperly() => ConsumeProperly(Repeats);

    [Benchmark]
    public ValueTask<int> ConsumeValueTaskCrazy() => ConsumeCrazy(Repeats);

    async Task<int> ConsumeTask(int repeats)
    {
        int total = 0;
        while (repeats-- > 0)
            total += await SampleUsageAsync();

        return total;
    }

    Task<int> SampleUsageAsync() => Task.FromResult(1);

    async ValueTask<int> ConsumeWrong(int repeats)
    {
        int total = 0;
        while (repeats-- > 0)
            total += await SampleUsage();

        return total;
    }

    async ValueTask<int> ConsumeProperly(int repeats)
    {
        int total = 0;
        while (repeats-- > 0)
        {
            ValueTask<int> valueTask = SampleUsage(); // INLINEABLE

            total += valueTask.IsCompleted
                ? valueTask.Result
                : await valueTask.AsTask();
        }

        return total;
    }

    ValueTask<int> ConsumeCrazy(int repeats)
    {
        int total = 0;
        while (repeats-- > 0)
        {
            ValueTask<int> valueTask = SampleUsage(); // INLINEABLE

            if (valueTask.IsCompleted)
                total += valueTask.Result;
            else
                return ContinueAsync(valueTask, repeats, total);
        }

        return new ValueTask<int>(total);
    }

    async ValueTask<int> ContinueAsync(ValueTask<int> valueTask, int repeats, int total)
    {
        total += await valueTask;

        while (repeats-- > 0)
        {
            valueTask = SampleUsage();

            if (valueTask.IsCompleted)
                total += valueTask.Result;
            else
                total += await valueTask;
        }

        return total;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] // super important!
    ValueTask<int> SampleUsage()
        => IsFastSynchronousExecutionPossible()
            ? new ValueTask<int>(

                result: ExecuteSynchronous()) // INLINEABLE!!!
            : new ValueTask<int>(
                task: ExecuteAsync());

    [MethodImpl(MethodImplOptions.NoInlining)]
    bool IsFastSynchronousExecutionPossible() => true;

    int ExecuteSynchronous() => 1;
    Task<int> ExecuteAsync() => Task.FromResult(1);
}