using BenchmarkDotNet.Attributes;

namespace AoC;

public class Day10
{
    private readonly string[] _fileLines = File.ReadAllLines("day10.txt");

    [Fact, Benchmark]
    public void PartOne() => Assert.Equal(512, CSharp.Day10.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo() => Assert.Equal(1045, CSharp.Day10.PartTwo(_fileLines));
}
