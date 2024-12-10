```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean      | Error    | StdDev   | Allocated  |
|-------- |----------:|---------:|---------:|-----------:|
| PartOne |  77.69 ms | 0.328 ms | 0.306 ms |  839.04 KB |
| PartTwo | 265.87 ms | 1.142 ms | 1.068 ms | 1602.49 KB |
