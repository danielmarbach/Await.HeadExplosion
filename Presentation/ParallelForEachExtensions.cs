using System.IO;

static class ParallelForEachExtensions
{
    public static void Explain(this ParallelForEach runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Similar to `Parallel.For` but this time foreach
");     
    }
}