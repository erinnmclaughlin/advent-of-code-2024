```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                         | Mean      | Error    | StdDev   | Ratio | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|------------------------------- |----------:|---------:|---------:|------:|--------:|--------:|--------:|----------:|------------:|
| PartOne_CSharp                 | 337.38 μs | 1.930 μs | 1.805 μs |  1.00 | 83.0078 | 41.5039 | 41.5039 | 775.35 KB |        1.00 |
| PartOne_CSharp_Optimized       |  60.22 μs | 0.872 μs | 0.816 μs |  0.18 | 41.6260 | 41.6260 | 41.6260 | 315.26 KB |        0.41 |
| PartOne_CSharp_Optimized_Again |  42.44 μs | 0.238 μs | 0.223 μs |  0.13 | 38.4521 | 38.4521 | 38.4521 | 158.18 KB |        0.20 |
