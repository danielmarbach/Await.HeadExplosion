using System;
using System.Globalization;

static class NotifyCompletionExtensions
{

    public static void PrintCurrentCulture(this NotifyCompletion runnable)
    {
        Console.WriteLine(CultureInfo.CurrentCulture);
    }
}
