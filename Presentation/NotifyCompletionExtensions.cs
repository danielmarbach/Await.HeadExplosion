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
        writer.WriteLine(@"      
- `ICriticalNotifyCompletion` helps to implement the awaiter pattern
- `IsCompleted` and `void GetResult()` or `TResult GetResult()` still have to be added by convention
- `OnCompleted` has to flow the execution context while `OnUnsafeCompleted` doesn't have to but most of the impl do
- `OnUnsafeCompleted` can be called from partially trusted code
");
    }
}
