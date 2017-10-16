using System.Threading.Tasks;

[Order(20)]
class MakeFunOfSwissPeople : IRunnable
{
    public Task Run()
    {
        this.PrintJoke();
        return Task.CompletedTask;
    }
}