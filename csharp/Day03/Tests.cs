using Xunit.Abstractions;

namespace AdventOfCode2024.Day03;

public sealed class Tests(ITestOutputHelper testOutput)
{
    [Fact]
    public void Day_03_Benchmarks() => testOutput.WriteLine(BenchmarkRunner.Run<Benchmarks>());
    
    [Fact]
    public void Day_03_Part_01() => Assert.Equal(161085926, Solution.SolvePartOne(ReadFile()));

    [Fact]
    public void Day_03_Part_02() => Assert.Equal(82045421, Solution.SolvePartTwo(ReadFile()));
    
    private static string ReadFile() => File.ReadAllText(Path.Combine("Day03", "input.txt"));
}
