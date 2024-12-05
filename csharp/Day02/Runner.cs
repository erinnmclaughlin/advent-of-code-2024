using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day02;

[MemoryDiagnoser]
public class Runner
{
    private readonly string[] _fileLines = File.ReadAllLines("day02.txt");

    [BenchmarkRunner]
    public void RunBenchmarks() => BenchmarkRunner.Run<Runner>();

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(Solution.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(Solution.PartTwo(_fileLines));

    private static void AssertPartOne(int answer) => Assert.Equal(407, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(459, answer);
}