```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method                   | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|------------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| PartOne_CSharp           |  89.27 μs | 0.917 μs | 0.857 μs | 17.5781 | 1.4648 | 143.99 KB |
| PartOne_CSharp_Optimized |  28.55 μs | 0.144 μs | 0.135 μs |  1.5564 | 0.0305 |  12.92 KB |
| PartTwo_CSharp           | 114.56 μs | 1.207 μs | 1.129 μs | 24.2920 | 4.7607 | 199.76 KB |
| PartTwo_CSharp_Optimized |  46.04 μs | 0.132 μs | 0.117 μs |  7.0190 | 1.1597 |  57.56 KB |
