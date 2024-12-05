using BenchmarkDotNet.Attributes;
using Xunit.Abstractions;

namespace AdventOfCode2024.Day01;

[MemoryDiagnoser]
public class Runner(ITestOutputHelper? output = null)
{
    private readonly string[] _fileLines = File.ReadAllLines(Path.Combine("Day01", "input.txt"));

    [BenchmarkRunner]
    public void Benchmarks()
    {
        var summary = BenchmarkRunner.Run<Runner>();
        output?.WriteLine(summary);
    }

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(Day01.PartOne(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(Day01.PartTwo(_fileLines));

    [Fact, Benchmark]
    public void PartOne_FSharp() => AssertPartOne(fsharp.day01.part1(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_FSharp() => AssertPartTwo(fsharp.day01.part2(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(1879048, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(21024792, answer);
}