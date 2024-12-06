```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method                   | Mean      | Error     | StdDev    | Gen0    | Allocated |
|------------------------- |----------:|----------:|----------:|--------:|----------:|
| PartOne_CSharp           | 20.385 ms | 0.1224 ms | 0.1145 ms | 31.2500 |  508428 B |
| PartOne_CSharp_Optimized |  1.573 ms | 0.0109 ms | 0.0102 ms |       - |     185 B |
| PartTwo_CSharp           | 33.375 ms | 0.6479 ms | 0.6364 ms | 62.5000 |  772065 B |
