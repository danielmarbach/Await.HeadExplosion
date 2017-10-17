using System;
using System.Globalization;
using System.IO;

static class NotifyCompletionExtensions
{

    public static void PrintCurrentCulture(this NotifyCompletion runnable)
    {
        Console.WriteLine(CultureInfo.CurrentCulture);
    }

    public static void Explain(this NotifyCompletion runnable, TextWriter writer)
    {
        writer.WriteLine(" - For advanced scenarios 'ICriticalNotifyCompletion' can be used.");
    }
}
