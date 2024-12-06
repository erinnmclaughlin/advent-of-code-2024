using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.Declared)]
public class Day06
{
    private readonly string[] _fileLines = File.ReadAllLines("day06.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => Assert.Equal(5129, CSharp.Day06.PartOne(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_CSharp() => Assert.Equal(1888, CSharp.Day06.PartTwo(_fileLines));
}