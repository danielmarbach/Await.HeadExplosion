using System.Threading.Tasks;

[Order(24)]
class CustomValueTaskSource : IRunnable
{
    public async Task Run()
    {
        var valueTask = new ValueTask(new CustomTaskSource(3), 1);
        for (var i = 0; i < 7; i++)
        {
            await valueTask;
        }

        var longValueTask = new ValueTask<long>(new CustomLongTaskSource(3), 1);
        for (var i = 0; i < 7; i++)
        {
            this.PrintResult(await longValueTask);
        }
    }
}