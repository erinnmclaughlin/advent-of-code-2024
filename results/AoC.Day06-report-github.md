```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                        | Mean            | Error        | StdDev       | Gen0         | Gen1        | Gen2        | Allocated      |
|------------------------------ |----------------:|-------------:|-------------:|-------------:|------------:|------------:|---------------:|
| PartOne_CSharp                |       328.98 μs |     1.179 μs |     1.103 μs |      83.0078 |     41.5039 |     41.5039 |      775.35 KB |
| PartOne_CSharp_Optimized      |        60.43 μs |     0.663 μs |     0.620 μs |      41.6260 |     41.6260 |     41.6260 |      315.26 KB |
| PartTwo_CSharp                | 4,618,895.29 μs | 7,490.094 μs | 7,006.238 μs | 1150000.0000 | 571000.0000 | 496000.0000 | 10036591.17 KB |
| PartTwo_CSharp_Optimized      |   272,380.39 μs | 1,895.072 μs | 1,772.651 μs |  240500.0000 | 240000.0000 | 240000.0000 |  1378091.89 KB |
