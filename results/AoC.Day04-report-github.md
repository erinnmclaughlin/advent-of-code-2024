```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                   | Mean      | Error    | StdDev   | Gen0     | Allocated |
|------------------------- |----------:|---------:|---------:|---------:|----------:|
| PartOne_CSharp           | 388.01 μs | 0.309 μs | 0.274 μs | 148.4375 | 1245184 B |
| PartOne_CSharp_Optimized |  42.12 μs | 0.231 μs | 0.216 μs |        - |     184 B |
| PartTwo_CSharp           |  33.88 μs | 0.172 μs | 0.161 μs |  13.8550 |  116080 B |
| PartTwo_CSharp_Optimized |  19.30 μs | 0.086 μs | 0.076 μs |        - |     184 B |
