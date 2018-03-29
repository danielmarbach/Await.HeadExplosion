# Await.HeadExplosion

## Value Tasks

``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical cores and 4 physical cores
Frequency=1945315 Hz, Resolution=514.0556 ns, Timer=TSC
.NET Core SDK=2.1.103
  [Host]   : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT
  ShortRun : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                   Method | Repeats |        Mean |        Error |       StdDev | Scaled | ScaledSD |   Gen 0 | Allocated |
|------------------------- |-------- |------------:|-------------:|-------------:|-------:|---------:|--------:|----------:|
|              **ConsumeTask** |     **100** |    **874.7 ns** |  **3,155.10 ns** |   **178.269 ns** |   **2.54** |     **0.43** |  **1.7328** |    **7272 B** |
|    ConsumeValueTaskWrong |     100 |    998.2 ns |  1,419.13 ns |    80.183 ns |   2.90 |     0.21 |       - |       0 B |
| ConsumeValueTaskProperly |     100 |    344.9 ns |    224.85 ns |    12.705 ns |   1.00 |     0.00 |       - |       0 B |
|    ConsumeValueTaskCrazy |     100 |    361.3 ns |     59.83 ns |     3.380 ns |   1.05 |     0.03 |       - |       0 B |
|                          |         |             |              |              |        |          |         |           |
|              **ConsumeTask** |    **1000** | **10,000.4 ns** | **49,937.52 ns** | **2,821.562 ns** |   **3.23** |     **0.76** | **17.1814** |   **72072 B** |
|    ConsumeValueTaskWrong |    1000 |  9,508.8 ns |  3,116.34 ns |   176.079 ns |   3.07 |     0.14 |       - |       0 B |
| ConsumeValueTaskProperly |    1000 |  3,103.0 ns |  2,864.84 ns |   161.869 ns |   1.00 |     0.00 |       - |       0 B |
|    ConsumeValueTaskCrazy |    1000 |  3,283.8 ns |  1,327.09 ns |    74.983 ns |   1.06 |     0.05 |       - |       0 B |

``` ini
BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical cores and 4 physical cores
Frequency=1945315 Hz, Resolution=514.0556 ns, Timer=TSC
.NET Core SDK=2.1.300-preview3-008414
  [Host]   : .NET Core 2.1.0-preview3-26327-02 (CoreCLR 4.6.26327.05, CoreFX 4.6.26327.03), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview3-26327-02 (CoreCLR 4.6.26327.05, CoreFX 4.6.26327.03), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3
WarmupCount=3

```
|                   Method | Repeats |        Mean |        Error |       StdDev | Scaled | ScaledSD |   Gen 0 | Allocated |
|------------------------- |-------- |------------:|-------------:|-------------:|-------:|---------:|--------:|----------:|
|              **ConsumeTask** | 100 |    748.2 ns |    352.92 ns |    19.941 ns |   1.91 |     0.05 |  1.7328 |    7272 B |
|    ConsumeValueTaskWrong |     100 |  1,186.9 ns |     55.53 ns |     3.138 ns |   3.03 |     0.04 |       - |       0 B |
| ConsumeValueTaskProperly |     100 |    391.2 ns |    120.52 ns |     6.810 ns |   1.00 |     0.00 |       - |       0 B |
|    ConsumeValueTaskCrazy |     100 |    276.1 ns |     64.72 ns |     3.657 ns |   0.71 |     0.01 |       - |       0 B |
|                          |         |             |              |              |        |          |         |           |
|              **ConsumeTask** |1000 |  7,340.3 ns |  3,718.01 ns |   210.074 ns |   1.96 |     0.05 | 17.1814 |   72072 B |
|    ConsumeValueTaskWrong |    1000 | 18,480.7 ns | 69,049.35 ns | 3,901.416 ns |   4.93 |     0.85 |       - |       0 B |
| ConsumeValueTaskProperly |    1000 |  3,747.5 ns |  1,227.71 ns |    69.368 ns |   1.00 |     0.00 |       - |       0 B |
|    ConsumeValueTaskCrazy |    1000 |  2,576.6 ns |    345.06 ns |    19.496 ns |   0.69 |     0.01 |       - |       0 B |

## Task Improvements

### Before

``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical cores and 4 physical cores
Frequency=1945315 Hz, Resolution=514.0556 ns, Timer=TSC
.NET Core SDK=2.1.300-preview3-008414
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT
  Job-HZSBKB : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT

Toolchain=.NET Core 2.0  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|  Method |      Mean |     Error |    StdDev | Scaled | Allocated |
|-------- |----------:|----------:|----------:|-------:|----------:|
|  Return | 15.576 ms | 0.4185 ms | 0.0236 ms |   1.00 |     528 B |
|  Simple | 15.568 ms | 0.8275 ms | 0.0468 ms |   1.00 |     744 B |
| Actions |  2.008 ms | 0.0756 ms | 0.0043 ms |   0.13 |     560 B |



### After

``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical cores and 4 physical cores
Frequency=1945315 Hz, Resolution=514.0556 ns, Timer=TSC
.NET Core SDK=2.1.300-preview3-008414
  [Host]     : .NET Core 2.1.0-preview3-26327-02 (CoreCLR 4.6.26327.05, CoreFX 4.6.26327.03), 64bit RyuJIT
  Job-SLQOHY : .NET Core 2.1.0-preview3-26327-02 (CoreCLR 4.6.26327.05, CoreFX 4.6.26327.03), 64bit RyuJIT

Toolchain=2.1-preview3  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|  Method |      Mean |     Error |    StdDev | Scaled | Allocated |
|-------- |----------:|----------:|----------:|-------:|----------:|
|  Return | 15.542 ms | 1.3313 ms | 0.0752 ms |   1.00 |     376 B |
|  Simple | 15.538 ms | 1.4433 ms | 0.0815 ms |   1.00 |     488 B |
| Actions |  1.939 ms | 0.4590 ms | 0.0259 ms |   0.12 |     350 B |



## Links

 * [Avoid async delegate allocation](https://github.com/dotnet/coreclr/pull/14178)
 * [Reduce allocations when async methods yield](https://github.com/dotnet/coreclr/pull/13105)
 * [State of the .NET Performance - Adam Sitnik](https://youtu.be/CSPSvBeqJ9c?t=37m38s)
