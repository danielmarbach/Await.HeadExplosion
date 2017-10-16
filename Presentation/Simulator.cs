using System;
using System.Threading;
using System.Threading.Tasks;
class Simulator 
{
    public event EventHandler Fired = delegate { };

    public void Start() 
    {
        Task.Delay(500).ContinueWith(t => OnFired());
    }

    void OnFired() 
    {
        Console.WriteLine($"Fire on {Thread.CurrentThread.ManagedThreadId}");
        this.Fired(this, EventArgs.Empty);
    }
}
