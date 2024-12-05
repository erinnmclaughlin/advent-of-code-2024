using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day01;

[MemoryDiagnoser]
public class Runner
{
    private readonly string[] _fileLines = File.ReadAllLines("day01.txt");

    [BenchmarkRunner]
    public void RunBenchmarks() => BenchmarkRunner.Run<Runner>();

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(Solution.PartOne(_fileLines));

    [Fact, Benchmark]
    public void PartOne_FSharp() => AssertPartOne(fsharp.day01.part1(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(Solution.PartTwo(_fileLines));

    [Fact, Benchmark]
    public void PartTwo_FSharp() => AssertPartTwo(fsharp.day01.part2(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(1879048, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(21024792, answer);
}