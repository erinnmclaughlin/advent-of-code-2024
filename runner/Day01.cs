namespace AoC;

public sealed class Day01
{
    [Theory]
    [InlineData("day01.example.txt", 11)]
    [InlineData("day01.txt", 1879048)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day01.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day01.example.txt", 11)]
    [InlineData("day01.txt", 1879048)]
    public void PartOne_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day01Optimized.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day01.example.txt", 31)]
    [InlineData("day01.txt", 21024792)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day01.PartTwo(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day01.example.txt", 31)]
    [InlineData("day01.txt", 21024792)]
    public void PartTwo_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day01Optimized.PartTwo(fileLines).Should().Be(expected);
    }
}