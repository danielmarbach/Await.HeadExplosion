using System;
using System;
using System.Globalization;

static class NotifyCompletionExtensions
{

    public static void PrintCurrentCulture(this NotifyCompletion runnable)
    {
        Console.WriteLine(CultureInfo.CurrentCulture);
    }

    public static void Explain(this NotifyCompletion runnable)
    {
        Console.WriteLine(" # For advanced scenarios 'ICriticalNotifyCompletion' can be used.");
    }
}
