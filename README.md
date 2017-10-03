# Await.HeadExplosion

## Value Tasks

``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4980HQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
.NET Core SDK=2.1.0-preview1-007228
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |                   Method | Repeats |        Mean |      Error |       StdDev |      Median | Scaled | ScaledSD |   Gen 0 | Allocated |
 |------------------------- |-------- |------------:|-----------:|-------------:|------------:|-------:|---------:|--------:|----------:|
 |              **ConsumeTask** |     **100** |  **1,097.1 ns** |   **7.562 ns** |     **7.073 ns** |  **1,098.4 ns** |   **2.79** |     **0.20** |  **1.1539** |    **7272 B** |
 |    ConsumeValueTaskWrong |     100 |  1,050.5 ns |  43.935 ns |    41.097 ns |  1,035.4 ns |   2.68 |     0.22 |       - |       0 B |
 | ConsumeValueTaskProperly |     100 |    394.6 ns |   9.606 ns |    28.323 ns |    394.7 ns |   1.00 |     0.00 |       - |       0 B |
 |    ConsumeValueTaskCrazy |     100 |    359.1 ns |  11.959 ns |    35.260 ns |    354.8 ns |   0.91 |     0.11 |       - |       0 B |
 |              **ConsumeTask** |    **1000** |  **9,307.1 ns** | **396.345 ns** | **1,091.649 ns** |  **9,501.1 ns** |   **2.00** |     **0.60** | **11.4441** |   **72072 B** |
 |    ConsumeValueTaskWrong |    1000 | 11,073.7 ns | 468.996 ns | 1,382.844 ns | 10,329.0 ns |   2.38 |     0.73 |       - |       0 B |
 | ConsumeValueTaskProperly |    1000 |  5,075.2 ns | 543.450 ns | 1,602.374 ns |  4,455.4 ns |   1.00 |     0.00 |       - |       0 B |
 |    ConsumeValueTaskCrazy |    1000 |  4,140.6 ns | 211.741 ns |   604.109 ns |  4,201.2 ns |   0.89 |     0.28 |       - |       0 B |


## Task Improvements

### Before

``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4980HQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  Job-MXURVS : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT

Toolchain=CoreCsProj  

```
 |  Method |     Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |---------:|----------:|----------:|-------:|----------:|
 |  Return | 15.61 ms | 0.0150 ms | 0.0140 ms |   1.00 |     528 B |
 |  Simple | 15.61 ms | 0.0184 ms | 0.0172 ms |   1.00 |     744 B |


### After

``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4980HQ CPU 2.80GHz (Haswell), ProcessorCount=8
Frequency=2728070 Hz, Resolution=366.5595 ns, Timer=TSC
.NET Core SDK=2.1.0-preview1-007228
  [Host]     : .NET Core 2.1.0-preview1-25719-04 (Framework 4.6.25718.02), 64bit RyuJIT
  Job-EGQCNX : .NET Core 2.1.0-preview1-25719-04 (Framework 4.6.25718.02), 64bit RyuJIT

Toolchain=CoreCsProj  

```
 |  Method |     Mean |     Error |    StdDev | Scaled | Allocated |
 |-------- |---------:|----------:|----------:|-------:|----------:|
 |  Return | 15.60 ms | 0.0213 ms | 0.0189 ms |   1.00 |     520 B |
 |  Simple | 15.60 ms | 0.0115 ms | 0.0107 ms |   1.00 |     736 B |


## Links

 * [Avoid async delegate allocation](https://github.com/dotnet/coreclr/pull/14178)
 * [Reduce allocations when async methods yield](https://github.com/dotnet/coreclr/pull/13105)
 * [State of the .NET Performance - Adam Sitnik](https://youtu.be/CSPSvBeqJ9c?t=37m38s)