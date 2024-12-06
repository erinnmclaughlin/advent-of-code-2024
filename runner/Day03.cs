using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.Declared)]
public class Day03
{
    private readonly string _fileText = File.ReadAllText("day03.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(CSharp.Day03.PartOne(_fileText));
    
    [Fact, Benchmark]
    public void PartOne_CSharp_Optimized() => AssertPartOne(CSharp.Day03Optimized.PartOne(_fileText));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(CSharp.Day03.PartTwo(_fileText));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp_Optimized() => AssertPartTwo(CSharp.Day03Optimized.PartTwo(_fileText));
    
    private static void AssertPartOne(int answer) => Assert.Equal(161085926, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(82045421, answer);
}