using BenchmarkDotNet.Attributes;

namespace AoC;

public class Day08
{
    private readonly string[] _fileLines = File.ReadAllLines("day08.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => Assert.Equal(240, CSharp.Day08.PartOne(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_CSharp() => Assert.Equal(955, CSharp.Day08.PartTwo(_fileLines));
}
