using System;
using System.Diagnostics;
using System.Threading.Tasks;

static class ShortcutStatemachineExtensions 
{
    public static async Task PrintStackInformation(this ShortcutStatemachine runnable, Func<Task> method) 
    {
        try
        {
            await method().ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            var stackTrace = new StackTrace(1, true);
            
            Console.WriteLine($"{method.Method.Name}: FrameCount {stackTrace.FrameCount} / Has AsyncMethodBuilder '{stackTrace.ToString().Contains("AsyncTaskMethodBuilder")}'");
        }
    }
}