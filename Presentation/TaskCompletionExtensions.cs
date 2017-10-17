using System;
using System.IO;
using System.Threading;

static class TaskCompletionExtensions
{
    public static void Explain(this TaskCompletion runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- `TaskCompletionSource<TResult>` is a handy tool to achieve complex interop and custom async scenarios
- It represents a custom task that can be controled and transitioned into the state you like
- Attention: Awaiter completes on thread that called `SetResult` or `TrySetResult`
- Use `TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously)` with .NET 4.6.2 or higher to opt-out from sync completion.

");
    }

    public static void PrintStart(this TaskCompletion runnable )
    {
        Console.WriteLine($"Start on {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void PrintEnd(this TaskCompletion runnable )
    {
        Console.WriteLine($"Continue on {Thread.CurrentThread.ManagedThreadId}");
    }
}