namespace AoC;

public sealed class Day02
{
    [Theory]
    [InlineData("day02.example.txt", 2)]
    [InlineData("day02.txt", 407)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day02.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day02.example.txt", 4)]
    [InlineData("day02.txt", 459)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day02.PartTwo(fileLines).Should().Be(expected);
    }
}