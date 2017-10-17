using System.IO;

static class TaskCompletionExtensions
{
    public static void Explain(this TaskCompletion runnable, TextWriter writer)
    {
        writer.WriteLine(" - 'TaskCompletionSource<TResult>' is a handy tool to achieve complex interop and custom async scenarios");
        writer.WriteLine(" - Attention: Awaiter completes on thread that called 'SetResult' or 'TrySetResult'");
        writer.WriteLine(" - Use TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously) with .NET 4.6.2 or higher to opt-out from sync completion.");
    }
}