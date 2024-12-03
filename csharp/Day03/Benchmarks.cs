using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day03;

[MemoryDiagnoser]
public class Benchmarks
{
    private string Input { get; set; } = string.Empty;

    [GlobalSetup]
    public void Setup() => Input = File.ReadAllText(Path.Combine("Day03", "input.txt"));

    [Benchmark]
    public void PartOne() => Solution.SolvePartOne(Input);

    [Benchmark]
    public void PartTwo() => Solution.SolvePartTwo(Input);
}