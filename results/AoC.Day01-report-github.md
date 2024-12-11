```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method                   | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|------------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| PartOne_CSharp_Optimized |  58.65 μs | 1.070 μs | 1.535 μs |  0.9766 |      - |   8.04 KB |
| PartTwo_CSharp_Optimized | 106.18 μs | 0.772 μs | 0.722 μs |  0.9766 |      - |   8.04 KB |
| PartOne_CSharp           | 109.20 μs | 2.008 μs | 1.878 μs | 14.4043 | 0.3662 |  117.7 KB |
| PartOne_FSharp           | 149.34 μs | 2.430 μs | 2.154 μs | 17.0898 | 0.7324 | 140.94 KB |
| PartTwo_FSharp           | 388.36 μs | 1.883 μs | 1.762 μs | 34.6680 | 0.9766 | 284.27 KB |
| PartTwo_CSharp           | 592.51 μs | 4.106 μs | 3.841 μs | 23.4375 |      - | 195.66 KB |
