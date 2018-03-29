using System;
using System.IO;
using System.Threading.Tasks;

public static class StackTracesOhMyExtensions
{
    public static async Task PrintStackTrace(this StackTracesOhMy runnable, Func<Task> function)
    {
        try
        {
            await function();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    public static async Task Level2to6(this StackTracesOhMy runnable)
    {
        await Level2(runnable);
    }

    static async Task Level2(StackTracesOhMy runnable)
    {
        await Level3(runnable);
    }

    static async Task Level3(StackTracesOhMy runnable)
    {
        await Level4(runnable);
    }

    static async Task Level4(StackTracesOhMy runnable)
    {
        await Level5(runnable);
    }

    static async Task Level5(StackTracesOhMy runnable)
    {
        await runnable.Level6();
    }

    public static void Explain(this StackTracesOhMy runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- With .NET Core 2.1 finally readable stack traces
```
   at StackTracesOhMy.Level6() in C:\p\Await.HeadExplosion\Presentation\StackTracesOhMy.cs:line 20
   ...
   at StackTracesOhMyExtensions.Level2to6() in C:\p\Await.HeadExplosion\Presentation\StackTracesOhMyExtensions.cs:line 21
   at StackTracesOhMy.Level1() in C:\p\Await.HeadExplosion\Presentation\StackTracesOhMy.cs:line 14
   at StackTracesOhMy.Run() in C:\p\Await.HeadExplosion\Presentation\StackTracesOhMy.cs:line 9
   at StackTracesOhMyExtensions.PrintStackTrace() in C:\p\Await.HeadExplosion\Presentation\StackTracesOhMyExtensions.cs:line 11
```
");
    }    
}