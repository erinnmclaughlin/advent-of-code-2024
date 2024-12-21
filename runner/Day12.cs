namespace AoC;

public sealed class Day12
{
    [Theory]
    [InlineData("day12.example.txt", 1930)]
    [InlineData("day12.txt", 1465968)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day12.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day12.example.txt", 1206)]
    [InlineData("day12.txt", 897702)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day12.PartTwo(fileLines).Should().Be(expected);
    }
}