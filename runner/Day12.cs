namespace AoC;

public class Day12
{
    private readonly string[] _input = File.ReadAllLines("day12.txt");

    [Fact]
    public void PartOne() => Assert.Equal(1465968, CSharp.Day12.PartOne(_input.AsSpan()));

    [Fact]
    public void PartTwo() => Assert.Equal(897702, CSharp.Day12.PartTwo(_input));
}