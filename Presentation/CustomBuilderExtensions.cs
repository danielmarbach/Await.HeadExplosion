using System;

static class CustomBuilderExtensions
{
    public static void PrintResult(this CustomBuilder runnable, int result)
    {
        Console.WriteLine($"Result: {result}");
    }
}