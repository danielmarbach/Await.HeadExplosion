using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public struct TaskkMethodBuilder<TResult>
{
    private AsyncTaskMethodBuilder<TResult> _methodBuilder;
    private int calls;
    static Random random = new Random();
    internal TResult _result;
    internal bool GotResult;
    private bool _useBuilder;

    public void SetResult(TResult result)
    {
        var next = random.Next();
        if (next % 2 == 0)
        {
            if (typeof(TResult) == typeof(int))
            {
                dynamic dResult = result;
                dResult += 1;
                result = dResult;
            }
        }

        if (_useBuilder)
        {
            _methodBuilder.SetResult(result);
        }
        else
        {
            _result = result;
            GotResult = true;
        }
    }

    public Taskk<TResult> Task => GotResult ? new Taskk<TResult>(_result) : new Taskk<TResult>(_methodBuilder.Task.ContinueWith(
        async t =>
        {
            var r = await t;
            await System.Threading.Tasks.Task.Delay(random.Next(200, 1000));
            return r;
        }).Unwrap());
    public static TaskkMethodBuilder<TResult> Create() => new TaskkMethodBuilder<TResult> { _methodBuilder = AsyncTaskMethodBuilder<TResult>.Create() };

    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
    {
        _methodBuilder.Start(ref stateMachine);
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        _methodBuilder.SetStateMachine(stateMachine);
    }

    public void SetException(Exception exception)
    {
        _methodBuilder.SetException(exception);
    }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
    {
        _useBuilder = true;
        _methodBuilder.AwaitOnCompleted(ref awaiter, ref stateMachine);
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
    {
        _useBuilder = true;
        _methodBuilder.AwaitOnCompleted(ref awaiter, ref stateMachine);
    }
}