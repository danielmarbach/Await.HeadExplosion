using System.Threading.Tasks;

namespace Presentation
{
    [Order(-1)]
    public class Introduction : IRunnable
    {
        public Task Run()
        {
            this.PrintStart();
            this.PrintEnd();
            return Task.CompletedTask;
        }
    }
}