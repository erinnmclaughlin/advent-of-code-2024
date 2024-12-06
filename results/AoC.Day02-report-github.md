```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method         | Mean     | Error   | StdDev  | Gen0     | Allocated  |
|--------------- |---------:|--------:|--------:|---------:|-----------:|
| PartOne_CSharp | 128.8 μs | 0.32 μs | 0.29 μs |  45.6543 |  373.34 KB |
| PartTwo_CSharp | 468.4 μs | 0.57 μs | 0.53 μs | 174.3164 | 1425.12 KB |
