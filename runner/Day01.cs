using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Day01
{
    private readonly string[] _fileLines = File.ReadAllLines("day01.txt");
    
    [Benchmark, Fact]
    public void PartOne_CSharp() => AssertPartOne(CSharp.Day01.PartOne(_fileLines));
    
    [Benchmark, Fact]
    public void PartOne_CSharp_Optimized() => AssertPartOne(CSharp.Day01Optimized.PartOne(_fileLines));
    
    [Benchmark, Fact]
    public void PartOne_FSharp() => AssertPartOne(fsharp.day01.part1(_fileLines));
    
    [Benchmark, Fact]
    public void PartTwo_CSharp() => AssertPartTwo(CSharp.Day01.PartTwo(_fileLines));
    
    [Benchmark, Fact]
    public void PartTwo_CSharp_Optimized() => AssertPartTwo(CSharp.Day01Optimized.PartTwo(_fileLines));
    
    [Benchmark, Fact]
    public void PartTwo_FSharp() => AssertPartTwo(fsharp.day01.part2(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(1879048, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(21024792, answer);
}