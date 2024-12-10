```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean         | Error     | StdDev    | Gen0     | Gen1     | Gen2     | Allocated  |
|-------- |-------------:|----------:|----------:|---------:|---------:|---------:|-----------:|
| PartOne |     487.1 μs |   2.37 μs |   2.22 μs | 110.3516 | 110.3516 | 110.3516 |  370.32 KB |
| PartTwo | 110,054.0 μs | 816.23 μs | 763.50 μs |        - |        - |        - | 1133.32 KB |
