using System;

static class ParallelForEachExtensions
{
    public static void Explain(this ParallelForEach runnable)
    {
        Console.WriteLine(" # Like 'Paralle.For' can be combined with 'Task.Run'");        
    }
}