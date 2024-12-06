using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.Declared)]
public class Day05
{
    private readonly string[] _fileLines = File.ReadAllLines("day05.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(CSharp.Day05.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(CSharp.Day05.PartTwo(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(5268, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(5799, answer);
}