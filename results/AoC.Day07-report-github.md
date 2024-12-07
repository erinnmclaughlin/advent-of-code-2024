```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method                   | Mean         | Error      | StdDev     | Gen0        | Gen1      | Allocated  |
|------------------------- |-------------:|-----------:|-----------:|------------:|----------:|-----------:|
| PartOne_CSharp           |    23.249 ms |  0.2107 ms |  0.1971 ms |   4718.7500 |   31.2500 |   37.86 MB |
| PartOne_CSharp_Optimized |     6.103 ms |  0.0671 ms |  0.0595 ms |   4570.3125 |  343.7500 |    36.3 MB |
| PartTwo_CSharp           | 1,276.912 ms | 12.0190 ms | 11.2426 ms | 169000.0000 | 1000.0000 | 1355.14 MB |
| PartTwo_CSharp_Optimized |   200.251 ms |  1.7282 ms |  1.5320 ms | 120666.6667 | 5333.3333 |   958.8 MB |
