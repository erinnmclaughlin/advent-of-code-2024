using BenchmarkDotNet.Attributes;

namespace AoC;

public class Day09
{
    private readonly string _fileContent = File.ReadAllText("day09.txt");

    [Fact, Benchmark]
    public void PartOne()
    {
        Assert.Equal(6262891638328, CSharp.Day09.PartOne(_fileContent));
    }

    [Fact, Benchmark]
    public void PartTwo()
    {
        Assert.Equal(6287317016845, CSharp.Day09.PartTwo(_fileContent));
    }
}
