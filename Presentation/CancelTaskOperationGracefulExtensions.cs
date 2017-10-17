using System.IO;

static class CancelTaskOperationGracefulExtensions
{
    public static void Explain(this CancelTaskOperationGraceful runnable, TextWriter writer)
    {
        writer.WriteLine(" - It is up to the implementor to decide whether exceptions should be observed by the caller");
        writer.WriteLine(" - For graceful shutdown scenarios the root task should not transition into 'canceled'");
    }
}