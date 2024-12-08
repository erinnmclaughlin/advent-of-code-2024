using BenchmarkDotNet.Attributes;

namespace AoC;

public class Day07
{
    private readonly string[] _fileLines = File.ReadAllLines("day07.txt");

    [Fact, Benchmark]
    public void PartOne_CSharp() => Assert.Equal(1289579105366, CSharp.Day07.PartOne(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_CSharp() => Assert.Equal(92148721834692, CSharp.Day07.PartTwo(_fileLines));
}