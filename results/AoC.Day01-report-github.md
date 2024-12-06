```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method         | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| PartOne_CSharp |  55.52 μs | 0.138 μs | 0.129 μs | 14.4043 | 0.3662 |  117.7 KB |
| PartOne_FSharp |  68.85 μs | 0.245 μs | 0.204 μs | 17.2119 | 0.3662 | 140.94 KB |
| PartTwo_CSharp | 357.22 μs | 0.612 μs | 0.573 μs | 23.9258 | 0.4883 | 195.66 KB |
| PartTwo_FSharp | 338.82 μs | 0.494 μs | 0.438 μs | 34.6680 | 0.4883 | 284.27 KB |
