```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                   | Mean      | Error     | StdDev    | Gen0    | Allocated |
|------------------------- |----------:|----------:|----------:|--------:|----------:|
| PartOne_CSharp           | 15.161 ms | 0.2686 ms | 0.2513 ms | 46.8750 |  508423 B |
| PartOne_CSharp_Optimized |  1.280 ms | 0.0103 ms | 0.0091 ms |       - |     185 B |
| PartTwo_CSharp           | 25.283 ms | 0.0235 ms | 0.0196 ms | 62.5000 |  772063 B |
