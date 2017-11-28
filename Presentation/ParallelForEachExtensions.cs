using System.IO;

static class ParallelForEachExtensions
{
    public static void Explain(this ParallelForEach runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Similar to `Parallel.For` but this time foreach
- Why is this behaving differently in comparison to `Parallel.For`?
");     
    }
}