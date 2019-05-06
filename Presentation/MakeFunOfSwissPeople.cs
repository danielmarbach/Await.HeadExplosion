using System.Threading.Tasks;

#if NETCOREAPP2_1
[Order(28)]
#endif
#if NETCOREAPP2_0
[Order(27)]
#endif
class MakeFunOfSwissPeople : IRunnable
{
    public Task Run()
    {
        this.PrintJoke();
        return Task.CompletedTask;
    }
}