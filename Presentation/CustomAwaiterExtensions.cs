using System.IO;

static class CustomAwaiterExtensions 
{
    public static void Explain(this CustomAwaiter runnable, TextWriter writer)
    {
        writer.WriteLine(@"
- Anything can be awaited with the `GetAwaiter` (istance|static) convention
- Presence of the method (even in the library) makes things awaitable
- i.ex. allow to `await Process.Start`
");
    }
}