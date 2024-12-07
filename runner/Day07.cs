using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.Declared)]
public class Day07
{
    //private readonly string[] _fileLines = File.ReadAllLines("day07.example.txt");
    private readonly string[] _fileLines = File.ReadAllLines("day07.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(CSharp.Day07.PartOne(_fileLines));

    [Fact, Benchmark]
    public void PartOne_CSharp_Optimized() => AssertPartOne(CSharp.Day07Optimized.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(CSharp.Day07.PartTwo(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp_Optimized() => AssertPartTwo(CSharp.Day07Optimized.PartTwo(_fileLines));
    
    private static void AssertPartOne(long value) => Assert.Equal(1289579105366, value);
    private static void AssertPartTwo(long value) => Assert.Equal(92148721834692, value);
}