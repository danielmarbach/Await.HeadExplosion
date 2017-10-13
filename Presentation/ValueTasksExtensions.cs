using System;
using System.IO;
using System.Threading.Tasks;
static class ValueTasksExtensions
{
    public static async Task LoopTenTimes(this ValueTasks runnable, Func<int, Task<int>> action)
    {
        int total = 0;
        for (int i = 0; i < 10; i++)
        {
            total += await action(i);
        }
        Console.WriteLine($"Result {total}");
    }

    public static async Task<int> LoadFromFileAndCache(this ValueTasks runnable, string key)
    {
        using (var stream = File.OpenText(@"Values.txt"))
        {
            string line;
            while ((line = await stream.ReadLineAsync()) != null)
            {
                var splitted = line.Split(Convert.ToChar(";"));
                var k = splitted[0];
                var v = Convert.ToInt32(splitted[1]);

                if (k != key)
                {
                    continue;
                }

                runnable.cachedValues.TryAdd(k, v);
                return v;
            }
        }
        return 0;
    }

    public static void PrintFastPath(this ValueTasks runnable, int i)
    {
        Console.WriteLine($"Fast path {i}.");
    }

    public static void PrintAsyncPath(this ValueTasks runnable, int i)
    {
        Console.WriteLine($"Async path {i}.");
    }
}
