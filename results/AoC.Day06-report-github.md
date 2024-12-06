```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method         | Mean           | Error        | StdDev       | Gen0         | Gen1        | Gen2        | Allocated      |
|--------------- |---------------:|-------------:|-------------:|-------------:|------------:|------------:|---------------:|
| PartOne_CSharp |       599.2 μs |      7.19 μs |      6.01 μs |      83.0078 |     82.0313 |     41.0156 |      775.34 KB |
| PartTwo_CSharp | 7,670,890.9 μs | 67,937.89 μs | 60,225.17 μs | 1150000.0000 | 887000.0000 | 496000.0000 | 10036405.78 KB |
