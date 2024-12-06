using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.Declared)]
public class Day04
{
    private readonly string[] _fileLines = File.ReadAllLines("day04.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(CSharp.Day04.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartOne_CSharp_Optimized() => AssertPartOne(CSharp.Day04Optimized.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(CSharp.Day04.PartTwo(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp_Optimized() => AssertPartTwo(CSharp.Day04Optimized.PartTwo(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(2500, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(1933, answer);
}