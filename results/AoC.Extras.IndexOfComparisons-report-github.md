```

BenchmarkDotNet v0.14.0, macOS Sonoma 14.7.1 (23H222) [Darwin 23.6.0]
Apple M3 Max, 1 CPU, 14 logical and 14 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 9.0.0 (9.0.24.52809), Arm64 RyuJIT AdvSIMD


```
| Method                            | Mean        | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------------------------- |------------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| String_AsSpan_IndexOf             |    398.9 ns |  0.32 ns |  0.25 ns |  0.99 |    0.01 |      - |         - |          NA |
| String_IndexOf                    |    402.3 ns |  6.23 ns |  5.52 ns |  1.00 |    0.02 |      - |         - |          NA |
| String_AsMemory_Span_IndexOf      |    414.5 ns |  1.79 ns |  1.67 ns |  1.03 |    0.01 |      - |         - |          NA |
| String_ToCharArray_AsSpan_IndexOf |  1,214.8 ns |  5.46 ns |  4.56 ns |  3.02 |    0.04 | 4.0646 |   34088 B |          NA |
| String_ToArray_AsSpan_IndexOf     | 14,317.2 ns | 33.14 ns | 31.00 ns | 35.59 |    0.47 | 4.0588 |   34120 B |          NA |
