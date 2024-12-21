namespace AoC;

public sealed class Day08
{
    [Theory]
    [InlineData("day08.example.txt", 14)]
    [InlineData("day08.txt", 240)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day08.PartOne(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day08.example.txt", 34)]
    [InlineData("day08.txt", 955)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day08.PartTwo(fileLines).Should().Be(expected);
    }
}
