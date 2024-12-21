namespace AoC;

public sealed class Day05
{
    [Theory]
    [InlineData("day05.example.txt", 143)]
    [InlineData("day05.txt", 5268)]
    public void PartOne_CSharp(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day05.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day05.example.txt", 143)]
    [InlineData("day05.txt", 5268)]
    public void PartOne_CSharp_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day05Optimized.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day05.example.txt", 123)]
    [InlineData("day05.txt", 5799)]
    public void PartTwo_CSharp(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day05.PartTwo(fileLines).Should().Be(expected);
    }
}