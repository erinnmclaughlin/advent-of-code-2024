using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day05;

[MemoryDiagnoser]
public class Runner
{
    private readonly string[] _fileLines = File.ReadAllLines("day05.txt");

    [BenchmarkRunner]
    public void RunBenchmarks() => BenchmarkRunner.Run<Runner>();

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(Solution.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(Solution.PartTwo(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(5268, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(5799, answer);
}