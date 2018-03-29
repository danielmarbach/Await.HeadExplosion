using System.Threading.Tasks;

[Order(24)]
class MakeFunOfSwissPeople : IRunnable
{
    public Task Run()
    {
        this.PrintJoke();
        return Task.CompletedTask;
    }
}