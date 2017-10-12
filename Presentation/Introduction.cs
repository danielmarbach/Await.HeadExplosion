using System.Threading.Tasks;

[Order(-1)]
class Introduction : IRunnable
{
    public Task Run()
    {
        this.PrintStart();
        this.PrintEnd();
        return Task.CompletedTask;
    }
}