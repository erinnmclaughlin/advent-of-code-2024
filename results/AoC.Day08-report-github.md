```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method         | Mean     | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|--------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| PartOne_CSharp | 20.81 μs | 0.145 μs | 0.136 μs | 1.5564 | 0.0305 |  12.83 KB |
| PartTwo_CSharp | 37.31 μs | 0.408 μs | 0.381 μs | 7.0190 | 0.9766 |  57.47 KB |
