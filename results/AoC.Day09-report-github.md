```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean     | Error   | StdDev  | Allocated  |
|-------- |---------:|--------:|--------:|-----------:|
| PartOne | 333.7 ms | 1.31 ms | 1.22 ms |  839.77 KB |
| PartTwo | 265.8 ms | 0.68 ms | 0.60 ms | 1823.34 KB |
