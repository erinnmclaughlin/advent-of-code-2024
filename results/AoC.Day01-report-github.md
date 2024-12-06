```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method         | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| PartOne_CSharp |  55.58 μs | 0.364 μs | 0.284 μs | 14.4043 | 0.3662 |  117.7 KB |
| PartOne_FSharp |  69.00 μs | 0.296 μs | 0.262 μs | 17.2119 | 0.3662 | 140.94 KB |
| PartTwo_CSharp | 357.78 μs | 0.493 μs | 0.411 μs | 23.9258 | 0.4883 | 195.66 KB |
| PartTwo_FSharp | 338.04 μs | 0.324 μs | 0.287 μs | 34.6680 | 0.4883 | 284.27 KB |
