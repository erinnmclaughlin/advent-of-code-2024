namespace AoC;

public sealed class Day03
{
    [Theory]
    [InlineData("day03.example1.txt", 161)]
    [InlineData("day03.txt", 161085926)]
    public void PartOne(string filePath, int expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day03.PartOne(fileText).Should().Be(expected);
    }

    [Theory]
    [InlineData("day03.example1.txt", 161)]
    [InlineData("day03.txt", 161085926)]
    public void PartOne_Optimized(string filePath, int expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day03Optimized.PartOne(fileText).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day03.example2.txt", 48)]
    [InlineData("day03.txt", 82045421)]
    public void PartTwo(string filePath, int expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day03.PartTwo(fileText).Should().Be(expected);
    }
    
    [Theory]
    [InlineData("day03.example2.txt", 48)]
    [InlineData("day03.txt", 82045421)]
    public void PartTwo_Optimized(string filePath, int expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day03Optimized.PartTwo(fileText).Should().Be(expected);
    }
}