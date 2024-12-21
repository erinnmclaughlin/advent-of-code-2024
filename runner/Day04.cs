namespace AoC;

public sealed class Day04
{
    [Theory]
    [InlineData("day04.example.txt", 18)]
    [InlineData("day04.txt", 2500)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day04.PartOne(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day04.example.txt", 18)]
    [InlineData("day04.txt", 2500)]
    public void PartOne_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day04Optimized.PartOne(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day04.example.txt", 9)]
    [InlineData("day04.txt", 1933)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day04.PartTwo(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day04.example.txt", 9)]
    [InlineData("day04.txt", 1933)]
    public void PartTwo_Optimized(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day04Optimized.PartTwo(fileLines).Should().Be(expected);
    }
}