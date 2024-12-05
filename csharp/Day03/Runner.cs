using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day03;

[MemoryDiagnoser]
public class Runner
{
    private readonly string _fileText = File.ReadAllText("day03.txt");

    [BenchmarkRunner]
    public void RunBenchmarks() => BenchmarkRunner.Run<Runner>();

    [Fact, Benchmark]
    public void PartOne_CSharp() => AssertPartOne(Solution.PartOne(_fileText));
    
    [Fact, Benchmark]
    public void PartOne_CSharp_Optimized() => AssertPartOne(SolutionOptimized.PartOne(_fileText));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp() => AssertPartTwo(Solution.PartTwo(_fileText));
    
    [Fact, Benchmark]
    public void PartTwo_CSharp_Optimized() => AssertPartTwo(SolutionOptimized.PartTwo(_fileText));
    
    private static void AssertPartOne(int answer) => Assert.Equal(161085926, answer);
    private static void AssertPartTwo(int answer) => Assert.Equal(82045421, answer);
}