namespace AoC;

public sealed class Day06
{
    [Theory]
    [InlineData("day06.example.txt", 41)]
    [InlineData("day06.txt", 5129)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day06.PartOne(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day06.example.txt", 41)]
    [InlineData("day06.txt", 5129)]
    public void PartOne_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day06Optimized.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day06.example.txt", 41)]
    [InlineData("day06.txt", 5129)]
    public void PartOne_Optimized_Again(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day06MoreOptimized.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day06.example.txt", 6)]
    [InlineData("day06.txt", 1888)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day06.PartTwo(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day06.example.txt", 6)]
    [InlineData("day06.txt", 1888)]
    public void PartTwo_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day06Optimized.PartTwo(fileLines).Should().Be(expected);
    }
}