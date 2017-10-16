using System;

static class ParallelForExtensions
{
    public static void Explain(this ParallelFor runnable)
    {
        Console.WriteLine(" # Parallel / Compute bound blocking work happens on the worker thread pool.");
        Console.WriteLine(" # CPU bound = One thread handles one CPU-bound task at a time");
        Console.WriteLine(" # Blocking");
    }
}