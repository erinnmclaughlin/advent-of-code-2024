```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method      | NumRounds | Mean            | Error        | StdDev       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------ |---------- |----------------:|-------------:|-------------:|----------:|----------:|----------:|------------:|
| **PartOneFast** | **1**         |      **1,243.1 ns** |      **1.95 ns** |      **1.63 ns** |    **0.4711** |    **0.0019** |         **-** |     **3.86 KB** |
| PartOneSlow | 1         |        370.4 ns |      2.07 ns |      1.93 ns |    0.1855 |         - |         - |     1.52 KB |
| **PartOneFast** | **10**        |     **52,862.6 ns** |    **123.68 ns** |    **109.64 ns** |   **14.4043** |    **0.6104** |         **-** |   **117.75 KB** |
| PartOneSlow | 10        |     15,867.9 ns |     41.00 ns |     38.35 ns |    6.4087 |    0.0916 |         - |    52.34 KB |
| **PartOneFast** | **25**        |    **384,581.0 ns** |  **1,335.83 ns** |  **1,184.18 ns** |   **97.6563** |   **11.2305** |         **-** |   **798.55 KB** |
| PartOneSlow | 25        | 12,355,022.4 ns | 61,468.09 ns | 54,489.86 ns | 2109.3750 | 1437.5000 | 1062.5000 | 21823.69 KB |
