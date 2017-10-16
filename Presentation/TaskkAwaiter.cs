using System;
using System.Runtime.CompilerServices;

public struct TaskkAwaiter<TResult> : ICriticalNotifyCompletion
{
    private readonly Taskk<TResult> _value;

    internal TaskkAwaiter(Taskk<TResult> value) => _value = value;

    public bool IsCompleted => _value.IsCompleted;

    public TResult GetResult() =>
        _value._task == null ?
            _value._result :
            _value._task.GetAwaiter().GetResult();

    public void OnCompleted(Action continuation) =>
        _value.AsTask().ConfigureAwait(continueOnCapturedContext: true).GetAwaiter().OnCompleted(continuation);

    /// <summary>Schedules the continuation action for this ValueTask.</summary>
    public void UnsafeOnCompleted(Action continuation) =>
        _value.AsTask().ConfigureAwait(continueOnCapturedContext: true).GetAwaiter().UnsafeOnCompleted(continuation);
}