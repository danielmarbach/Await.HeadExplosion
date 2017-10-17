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

## SequentialExecution
 - Lazy nature of enumerable creates tasks when iterating
 - 'Await' means sequentialize here
## ConcurrentExecution
 - 'Task.WhenAll' materializes enumerable
 - Tasks are executed concurrently
 - WhenAll task is done when all done
## ParallelExecution
 - Nature of Task API allows to combine concurrency and explicit parallelism.
 - Degree of Parallelism = Number of Threads used from worker pool.
## Unwrapping
 - Async in `Task.Factory.StartNew` returns a proxy task `Task<Task>`
 - Proxy task is completed before the actual task is completed
## CancelTask
 - Passing a token to a task does only impact the final state of the task
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
 |  Method |     Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |---------:|----------:|----------:|-------:|----------:|
 |  Return | 15.61 ms | 0.0150 ms | 0.0140 ms |   1.00 |     528 B |
 |  Simple | 15.61 ms | 0.0184 ms | 0.0172 ms |   1.00 |     744 B |
 
 - NET Core 2.1 preview:
 |  Method |     Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |---------:|----------:|----------:|-------:|----------:|
 |  Return | 15.61 ms | 0.0150 ms | 0.0140 ms |   1.00 |     520 B |
 |  Simple | 15.61 ms | 0.0184 ms | 0.0172 ms |   1.00 |     736 B |
 
## TaskFactoryStartNewLongRunning
 - 'LongRunning' flag is a waste in combination with an async body
 - Don't try to be smarter than the TPL ;)
## ConcurrencyLimit
 - 'SemaphoreSlim' is a handy structure to limit concurrency
 - 'SemaphoreSlim' does not preserve order
 - 'SemaphoreSlim' can be used as async lock structure if required (caveat 100 times slower than lock)
## ThreadLimit
 - 'TaskScheduler.Current' is floated into async continuations with `Task.Factory.StartNew`
 - 'ConfigureAwait(false)' or 'TaskCreationOptions.HideScheduler' allows to opt-out
 - I would quit the project if you forced me to maintain this code ;)
 - If you think you need a scheduler you are probably doing it wrong ;)
## TaskCompletion
 - 'TaskCompletionSource<TResult>' is a handy tool to achieve complex interop and custom async scenarios
 - Attention: Awaiter completes on thread that called 'SetResult' or 'TrySetResult'
 - Use TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously) with .NET 4.6.2 or higher to opt-out from sync completion.
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
        
## CustomAwaiter
 - Anything can be awaited with the `GetAwaiter` (istance|static) convention
 - Presence of the method (even in the library) makes things awaitable
 - i.ex. allow to await Process.Start
## NotifyCompletion
 - For advanced scenarios 'ICriticalNotifyCompletion' can be used.
## CustomBuilder
 - Category useless knowledge
 - Make fun of your coworkers
## MakeFunOfSwissPeople
