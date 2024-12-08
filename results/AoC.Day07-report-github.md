```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method         | Mean       | Error     | StdDev    | Gen0        | Gen1      | Allocated |
|--------------- |-----------:|----------:|----------:|------------:|----------:|----------:|
| PartOne_CSharp |   5.369 ms | 0.0440 ms | 0.0390 ms |   4570.3125 |  335.9375 |   36.3 MB |
| PartTwo_CSharp | 180.982 ms | 0.8674 ms | 0.8114 ms | 120666.6667 | 5333.3333 |  958.8 MB |
