using System.Threading.Tasks;

[Order(5)]
class SimpleAsync : IRunnable
{
    public async Task Run()
    {
        this.PrintStart();
        await Task.Delay(1000);
        this.PrintEnd();
    }
}