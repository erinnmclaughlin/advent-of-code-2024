using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day04;

[MemoryDiagnoser]
public class Runner
{
    private readonly string[] _fileLines = File.ReadAllLines("day04.txt");

    [BenchmarkRunner]
    public void RunBenchmarks() => BenchmarkRunner.Run<Runner>();

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(Solution.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartOne_CSharp_Optimized() => AssertPartOne(Day04Optimized.PartOne(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(Solution.PartTwo(_fileLines));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp_Optimized() => AssertPartTwo(Day04Optimized.PartTwo(_fileLines));
    
    private static void AssertPartOne(int answer) => Assert.Equal(2500, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(1933, answer);
}