```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                   | Mean      | Error    | StdDev   | Gen0     | Allocated |
|------------------------- |----------:|---------:|---------:|---------:|----------:|
| PartOne_CSharp           | 367.53 μs | 0.574 μs | 0.480 μs | 148.4375 | 1245184 B |
| PartOne_CSharp_Optimized |  41.99 μs | 0.342 μs | 0.303 μs |        - |     184 B |
| PartTwo_CSharp           |  33.97 μs | 0.094 μs | 0.088 μs |  13.8550 |  116080 B |
| PartTwo_CSharp_Optimized |  19.24 μs | 0.053 μs | 0.047 μs |        - |     184 B |
