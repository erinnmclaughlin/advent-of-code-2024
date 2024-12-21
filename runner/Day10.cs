namespace AoC;

public sealed class Day10
{
    [Theory]
    [InlineData("day10.example.txt", 36)]
    [InlineData("day10.txt", 512)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day10.PartOne(fileLines).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day10.example.txt", 81)]
    [InlineData("day10.txt", 1045)]
    public void PartTwo(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day10.PartTwo(fileLines).Should().Be(expected);
    }
}
