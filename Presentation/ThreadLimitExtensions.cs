using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
static class ThreadLimitExtensions 
{
    public static void PrintStartNewBefore(this ThreadLimit runnable, int current)
    {
        var scheduler = TaskScheduler.Current as LimitedConcurrencyLevelTaskScheduler;
        if (scheduler != null)
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](Limit): start Task.Factory.StartNew({current})");
        }
        else
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](none): start Task.Factory.StartNew({current})");
        }
    }

    public static void PrintStartNewAfter(this ThreadLimit runnable, int current)
    {
        var scheduler = TaskScheduler.Current as LimitedConcurrencyLevelTaskScheduler;
        if (scheduler != null)
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](Limit): done Task.Factory.StartNew({current})");
        }
        else
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](none): done Task.Factory.StartNew({current})");
        }
    }    

    public static void PrintRunBefore(this ThreadLimit runnable, int current)
    {
        var scheduler = TaskScheduler.Current as LimitedConcurrencyLevelTaskScheduler;
        if (scheduler != null)
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](Limit): start Task.Run({current})");
        }
        else
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](none): start Task.Run({current})");
        }
    }

    public static void PrintRunAfter(this ThreadLimit runnable, int current)
    {
        var scheduler = TaskScheduler.Current as LimitedConcurrencyLevelTaskScheduler;
        if (scheduler != null)
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](Limit): done Task.Run({current})");
        }
        else
        {
            Console.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId}](none): done Task.Run({current})");
        }
    }      

    public static void PrintOptions(this ThreadLimit runnable, TaskCreationOptions options, bool configureAwait) 
    {
        Console.WriteLine($"Running with creation options {options} and ConfigureAwait({configureAwait})");
    }

    public static async Task PumpWithSemaphoreConcurrencyTwo(this ThreadLimit runnable, Func<int, CancellationToken, Task> action) 
    {
        var maxConcurrency = 2;
        var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        var tokenSource = new CancellationTokenSource();
        tokenSource.CancelAfter(TimeSpan.FromSeconds(2));
        var token = tokenSource.Token;
        var pumpTask = Task.Run(async () =>
        {
            int numberOfTasks = 0;
            while (!token.IsCancellationRequested)
            {
                await semaphore.WaitAsync(token);

                action(numberOfTasks++, token).ContinueWith((t, state) =>
                {
                    var s = (SemaphoreSlim)state;
                    s.Release();
                }, semaphore, TaskContinuationOptions.ExecuteSynchronously)
                .Ignore();
            }
        });
        
        async Task WaitForPendingWork() 
        {
            while (semaphore.CurrentCount != maxConcurrency)
            {
                await Task.Delay(50);
            }
        }
        await pumpTask.IgnoreCancellation();
        await WaitForPendingWork();
        Console.WriteLine("...");
        Console.ReadLine();
    }

    public static void Explain(this ThreadLimit runnable, TextWriter writer) 
    {
        writer.WriteLine(@"
- `TaskScheduler.Current` is floated into async continuations with `Task.Factory.StartNew`
- `ConfigureAwait(false)` or `TaskCreationOptions.HideScheduler` allows to opt-out
- I would quit the project if you forced me to maintain this code ;)
- If you think you need a scheduler you are probably doing it wrong ;)
");
    }       
}
