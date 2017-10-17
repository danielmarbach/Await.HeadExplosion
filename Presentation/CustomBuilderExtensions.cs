using System;

static class CustomBuilderExtensions
{
    public static void PrintResult(this CustomBuilder runnable, int result)
    {
        Console.WriteLine($"Result: {result}");
    }

    public static void Explain( this CustomBuilder runnable)
    {
        Console.WriteLine(" # Category useless knowledge");
        Console.WriteLine(" # Make fun of your coworkers");
    }
}