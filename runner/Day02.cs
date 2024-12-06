using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.Declared)]
public class Day02
{
    private readonly string[] _fileLines = File.ReadAllLines("day02.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(CSharp.Day02.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(CSharp.Day02.PartTwo(_fileLines));

    private static void AssertPartOne(int answer) => Assert.Equal(407, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(459, answer);
}