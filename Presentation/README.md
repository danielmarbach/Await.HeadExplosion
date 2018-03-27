## Introduction

- Each sample is contained in a runnable class
- Extension methods are used to hide non important details
- `Thread(s)` view will show the last used thread to render the console also refered to as the `main` thread

## ParallelFor

- `CpuBound.Compute` contains a quick sort algorithm
- `Parallel.For` is here to divide and conquer compute bound problems faster by applying parallelism
- Operations are scheduled on the worker thread pool
- Multiple arrays of length 5 to 10 will be sorted in parallel
- Parallel.For is a blocking operation

## ParallelForEach

- Similar to `Parallel.For` but this time foreach
- Why is this behaving differently in comparison to `Parallel.For`?

## TaskRun

- Useful when blocking and compute bound operations should be offloaded to the worker thread pool
- One thread from the pool per `Task.Run` when no async body is used
- Prefered over `Task.Factory.StartNew` because it applies reasonable defaults for 99% of the use cases

## TaskFactoryStartNew

- Useful when blocking and compute bound operations should be offloaded to the worker thread pool
- Useful when finer control is required
- One thread from the pool per `Task.Factory.StartNew` when no async body is used
- Uses `TaskScheduler.Current` by default if you don't specify one which can be a scheduler that wraps the SynchronizationContext
- Is the shoot yourself in the foot API if you don't know what you are doing (more examples later)

## SimpleAsync

- Every `await` statement is a chance for the calling thread to do something else
- Much more efficient due to less thread usage
- Can achieve higher saturation of ressources available

## AsyncAllTheWay

- It is OK to call sync code from async context
- It defeats the purpose of async when async is called from sync code
- It is dangerous to call async code from sync code, it can lead to deadlocks.

## ConfigureAwait

- `ConfigureAwait` controls whether context capturing is enabled
- Context capturing as a simplification can be understood as restoring the TaskScheduler that was visible before the `await`
- Context capturing affects the continuation of an asynchronous method

```
await Method().ConfigureAwait(true|false);
await Continuation(); // <-- affected by line above
```

## SequentialExecution

- `Task.Delay` represents a true asynchronous operation, no offloading needed
- Protip: `Task.Delay` uses a timer and is subjected to timer resolution on the system
- Lazy nature of enumerable creates tasks when iterating
- `Await` means sequentialize here

## ConcurrentExecution

- `Task.WhenAll` materializes enumerable
- Tasks are executed concurrently
- `Task.WhenAll` task is done when all done
- When one task threw an exception the task is faulted and the exception rethrown
- When interested in the outcome of each task you have to loop over the tasks

## ParallelExecution

- Nature of Task API allows to combine concurrency and explicit parallelism.
- Degree of Parallelism = Number of Threads used from worker pool.
- Async all the way: Try to avoid blocking code in async body if you can
- In some scenarios it is OK to call blocking IO bound code in async body
- Top level caller can always offload if required

## Unwrapping

- Async in `Task.Factory.StartNew` returns a proxy task `Task<Task>`
- Proxy task is completed before the actual task is completed
- Can lead to interesting bugs (seen in the wild many times)

## CancelTask

- Passing a token to a task does only impact the final state of the task
- Cancellation is a cooperative effort

## CancelTaskOperation

- Cooperative cancelation means the token has to be observed by the implementor

## CancelTaskOperationGraceful

- It is up to the implementor to decide whether exceptions should be observed by the caller
- For graceful shutdown scenarios the root task should not transition into 'canceled'

## ShortcutStatemachine

- For highperf scenario `async` keyword can be omitted
- Apply carefully and only after measuring
- For most scenarios apply the keyword since it prevents mistakes
- NET Core 2.0:

 |  Method |      Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |----------:|----------:|----------:|-------:|----------:|
 |  Return | 15.576 ms | 0.4185 ms | 0.0236 ms |   1.00 |     528 B |
 |  Simple | 15.568 ms | 0.8275 ms | 0.0468 ms |   1.00 |     744 B |
 | Actions |  2.008 ms | 0.0756 ms | 0.0043 ms |   0.13 |     560 B |

- NET Core 2.1 preview:

 |  Method |      Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |----------:|----------:|----------:|-------:|----------:|
 |  Return | 15.542 ms | 1.3313 ms | 0.0752 ms |   1.00 |     376 B |
 |  Simple | 15.538 ms | 1.4433 ms | 0.0815 ms |   1.00 |     488 B |
 | Actions |  1.939 ms | 0.4590 ms | 0.0259 ms |   0.12 |     350 B |


## TaskFactoryStartNewLongRunning

- `TaskCreationOptions.LongRunning` instruct the TPL to create a background thread (non-pooled thread)                
- First `await` statement will return the background (non-pooled) thread, waste is generated
- Useful only for long-running loops without async body
- Don't try to be smarter than the TPL ;)

## ConcurrencyLimit

- `SemaphoreSlim` is a handy structure to limit concurrency
- `SemaphoreSlim` does not preserve order
- `SemaphoreSlim` can be used as async lock structure if required (caveat at least 10 times slower than lock)

 |                   Method | Overhead |
 |------------------------- |---------:|
 |          **lock**        |  20 ns   | 
 |       SemaphoreSlim      |  200 ns  |
 |         Semaphore        |  1000 ns |

http://www.albahari.com/threading/part2.aspx

## ThreadLimit

- `TaskScheduler.Current` is floated into async continuations with `Task.Factory.StartNew`
- `ConfigureAwait(false)` or `TaskCreationOptions.HideScheduler` allows to opt-out
- I would quit the project if you forced me to maintain this code ;)
- If you think you need a scheduler you are probably doing it wrong ;)

## TaskCompletion

- `TaskCompletionSource<TResult>` is a handy tool to achieve complex interop and custom async scenarios
- It represents a custom task that can be controled and transitioned into the state you like
- Attention: Awaiter completes on thread that called `SetResult` or `TrySetResult`
- Use `TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously)` with .NET 4.6.2 or higher to opt-out from sync completion.


## ValueTasks

- Nice for highperf scenarios and only then!
- Complex to use and easy to get wrong
- Stats:

 |                   Method | Repeats |        Mean |      Error |       StdDev |      Median | Scaled | ScaledSD |   Gen 0 | Allocated |
 |------------------------- |-------- |------------:|-----------:|-------------:|------------:|-------:|---------:|--------:|----------:|
 |          **ConsumeTask** |    1000 |  9,307.1 ns | 396.345 ns | 1,091.649 ns |  9,501.1 ns |   2.00 |     0.60 | 11.4441 |   72072 B |
 |    ConsumeValueTaskWrong |    1000 | 11,073.7 ns | 468.996 ns | 1,382.844 ns | 10,329.0 ns |   2.38 |     0.73 |       - |       0 B |
 | ConsumeValueTaskProperly |    1000 |  5,075.2 ns | 543.450 ns | 1,602.374 ns |  4,455.4 ns |   1.00 |     0.00 |       - |       0 B |
 |    ConsumeValueTaskCrazy |    1000 |  4,140.6 ns | 211.741 ns |   604.109 ns |  4,201.2 ns |   0.89 |     0.28 |       - |       0 B |        

https://github.com/adamsitnik/StateOfTheDotNetPerformance        

## CustomAwaiter

- Anything can be awaited with the `GetAwaiter` (istance|static) convention
- Presence of the method (even in the library) makes things awaitable
- i.ex. allow to `await Process.Start`

## NotifyCompletion
      
- `ICriticalNotifyCompletion` helps to implement the awaiter pattern
- `IsCompleted` and `void GetResult()` or `TResult GetResult()` still have to be added by convention
- `OnCompleted` has to flow the execution context while `OnUnsafeCompleted` doesn't have to but most of the impl do
- `OnUnsafeCompleted` can be called from partially trusted code

## CustomBuilder

- Category useless knowledge
- Make fun of your coworkers

## MakeFunOfSwissPeople
