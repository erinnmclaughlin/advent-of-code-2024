```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean      | Error    | StdDev   | Allocated  |
|-------- |----------:|---------:|---------:|-----------:|
| PartOne |  77.52 ms | 0.412 ms | 0.386 ms |  370.31 KB |
| PartTwo | 264.16 ms | 1.300 ms | 1.216 ms | 1133.44 KB |
