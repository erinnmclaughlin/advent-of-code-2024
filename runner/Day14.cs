namespace AoC;

public sealed class Day14
{
    [Theory]
    [InlineData("day14.example.txt", 7, 11, 12)]
    [InlineData("day14.txt", 103, 101, 216772608)]
    public void PartOne(string filePath, int height, int width, long expected)
    {
        var fileLines = File.ReadAllLines(filePath);
        CSharp.Day14.PartOne(fileLines, 100, height, width).Should().Be(expected);
    }
}
