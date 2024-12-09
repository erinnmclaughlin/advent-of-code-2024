```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean     | Error   | StdDev  | Allocated  |
|-------- |---------:|--------:|--------:|-----------:|
| PartOne | 335.4 ms | 6.39 ms | 8.31 ms |  840.13 KB |
| PartTwo | 265.4 ms | 0.62 ms | 0.55 ms | 1823.34 KB |
