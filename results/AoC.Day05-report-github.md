```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method         | Mean     | Error    | StdDev   | Gen0    | Allocated |
|--------------- |---------:|---------:|---------:|--------:|----------:|
| PartOne_CSharp | 15.09 ms | 0.017 ms | 0.016 ms | 46.8750 | 496.51 KB |
| PartTwo_CSharp | 25.48 ms | 0.050 ms | 0.044 ms | 62.5000 | 753.97 KB |
