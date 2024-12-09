```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean     | Error   | StdDev  | Allocated  |
|-------- |---------:|--------:|--------:|-----------:|
| PartOne | 327.0 ms | 1.47 ms | 1.38 ms |  839.55 KB |
| PartTwo | 265.0 ms | 0.87 ms | 0.81 ms | 1602.57 KB |
