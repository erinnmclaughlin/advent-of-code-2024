```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                   | Mean      | Error    | StdDev   | Gen0    | Gen1    | Allocated |
|------------------------- |----------:|---------:|---------:|--------:|--------:|----------:|
| PartOne_CSharp           | 102.40 μs | 0.352 μs | 0.312 μs | 48.3398 | 17.0898 |  405280 B |
| PartOne_CSharp_Optimized |  15.06 μs | 0.029 μs | 0.027 μs |  0.0153 |       - |     184 B |
| PartTwo_CSharp           | 102.45 μs | 0.309 μs | 0.289 μs | 46.3867 | 21.3623 |  388392 B |
| PartTwo_CSharp_Optimized |  20.17 μs | 0.013 μs | 0.012 μs |       - |       - |     184 B |
