using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[AsyncMethodBuilder(typeof(TaskkMethodBuilder<>))]
public struct Taskk<TResult>
{
    public override int GetHashCode()
    {
        unchecked
        {
            return (EqualityComparer<TResult>.Default.GetHashCode(_result) * 397) ^ _hasResult.GetHashCode();
        }
    }

    internal readonly TResult _result;
    internal readonly bool _hasResult;
    internal readonly Task<TResult> _task;

    public Taskk(Task<TResult> task)
    {
        _result = default(TResult);
        _task = task;
        _hasResult = false;
    }

    public Taskk(TResult result)
    {
        _result = result;
        _hasResult = true;
        _task = null;
    }

    public bool HasValue => _hasResult;
    public bool Equals(Taskk<TResult> other) => !_hasResult && !other._hasResult || _hasResult && other._hasResult && _result.Equals(other._result);
    public override bool Equals(object obj) => obj is Taskk<TResult> && Equals((Taskk<TResult>)obj);
    public static bool operator ==(Taskk<TResult> left, Taskk<TResult> right) => left.Equals(right);
    public static bool operator !=(Taskk<TResult> left, Taskk<TResult> right) => !left.Equals(right);
    public override string ToString() => _hasResult ? _result.ToString() : "";
    public bool IsCompleted => _task == null || _task.IsCompleted;

    public bool IsCompletedSuccessfully => _task == null || _task.Status == TaskStatus.RanToCompletion;

    public bool IsFaulted => _task != null && _task.IsFaulted;

    public bool IsCanceled => _task != null && _task.IsCanceled;

    public TResult Result => _task == null ? _result : _task.GetAwaiter().GetResult();

    public Task<TResult> AsTask() => _task ?? Task.FromResult(_result);

    public TaskkAwaiter<TResult> GetAwaiter() => new TaskkAwaiter<TResult>(this);

    public ConfiguredTaskkAwaiter<TResult> ConfigureAwait(bool continueOnCapturedContext) =>
            new ConfiguredTaskkAwaiter<TResult>(this, continueOnCapturedContext: continueOnCapturedContext);

    [EditorBrowsable(EditorBrowsableState.Never)] // intended only for compiler consumption
    public static TaskkMethodBuilder<TResult> CreateAsyncMethodBuilder() => TaskkMethodBuilder<TResult>.Create();
}