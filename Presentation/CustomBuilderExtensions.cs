using System;
using System.IO;

static class CustomBuilderExtensions
{
    public static void PrintResult(this CustomBuilder runnable, int result)
    {
        Console.WriteLine($"Result: {result}");
    }

    public static void Explain( this CustomBuilder runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Category useless knowledge
- Make fun of your coworkers
");
    }
}