using System.Threading.Tasks;

[Order(18)]
class CustomBuilder : IRunnable
{
    public async Task Run()
    {
        var result = await Calculate();
        this.PrintResult(result);
    }

    static async Taskk<int> Calculate()
    {
        int value = 0;
        value += await GetValue();
        value += await GetValue();
        value += await GetValue();
        return value;
    }

    static async Taskk<int> GetValue()
    {
        await Task.Delay(1);
        return 1;
    }
}