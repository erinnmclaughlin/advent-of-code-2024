```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean     | Error   | StdDev  | Gen0     | Gen1   | Allocated |
|-------- |---------:|--------:|--------:|---------:|-------:|----------:|
| PartOne | 291.7 μs | 0.67 μs | 0.56 μs | 109.8633 | 0.4883 | 898.04 KB |
| PartTwo | 269.5 μs | 0.52 μs | 0.49 μs | 102.5391 |      - | 840.52 KB |
