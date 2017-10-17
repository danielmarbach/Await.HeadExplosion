using System.IO;

static class ParallelForExtensions
{
    public static void Explain(this ParallelFor runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- `CpuBound.Compute` contains a quick sort algorithm
- `Parallel.For` is here to divide and conquer compute bound problems faster by applying parallelism
- Operations are scheduled on the worker thread pool
- Multiple arrays of length 5 to 10 will be sorted in parallel
- Parallel.For is a blocking operation
");
    }
}