using System;

static class CustomAwaiterExtensions 
{
    public static void Explain(this CustomAwaiter runnable)
    {
        Console.WriteLine(" # Anything can be awaited with the `GetAwaiter` (istance|static) convention");
        Console.WriteLine(" # Presence of the method (even in the library) makes things awaitable");
        Console.WriteLine(" # i.ex. allow to await Process.Start");
    }
}