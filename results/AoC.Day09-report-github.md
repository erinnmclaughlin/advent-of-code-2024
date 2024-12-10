```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method  | Mean        | Error     | StdDev    | Gen0    | Gen1    | Gen2    | Allocated |
|-------- |------------:|----------:|----------:|--------:|--------:|--------:|----------:|
| PartOne |    462.0 μs |   1.35 μs |   1.26 μs | 58.5938 | 58.5938 | 58.5938 | 185.28 KB |
| PartTwo | 53,588.7 μs | 565.87 μs | 529.32 μs |       - |       - |       - | 862.25 KB |
