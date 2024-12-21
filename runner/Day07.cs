namespace AoC;

public sealed class Day07
{
    [Theory]
    [InlineData("day07.example.txt", 3749)]
    [InlineData("day07.txt", 1289579105366)]
    public void PartOne(string filePath, long expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day07.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day07.example.txt", 11387)]
    [InlineData("day07.txt", 92148721834692)]
    public void PartTwo(string filePath, long expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day07.PartTwo(fileLines).Should().Be(expected);
    }
}