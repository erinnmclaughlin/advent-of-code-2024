namespace AoC;

public sealed class Day13
{
    [Theory]
    [InlineData("day13.txt", 36954)]
    public void PartOne(string filePath, int expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day13.PartOne(fileLines).Should().Be(expected);
    }

    [Theory]
    [InlineData("day13.txt", 79352015273424)]
    public void PartTwo(string filePath, long expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day13.PartTwo(fileLines).Should().Be(expected);
    }
}