using System.Threading.Tasks;

#if NETCOREAPP2_1
[Order(26)]
#endif
#if NETCOREAPP2_0
[Order(25)]
#endif
class MakeFunOfSwissPeople : IRunnable
{
    public Task Run()
    {
        this.PrintJoke();
        return Task.CompletedTask;
    }
}