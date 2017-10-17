using System;

static class CancelTaskOperationGracefulExtensions
{
    public static void Explain(this CancelTaskOperationGraceful runnable)
    {
        Console.WriteLine(" # It is up to the implementor to decide whether exceptions should be observed by the caller");
        Console.WriteLine(" # For graceful shutdown scenarios the root task should not transition into 'canceled'");
    }
}