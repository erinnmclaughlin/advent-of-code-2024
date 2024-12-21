namespace AoC;

public sealed class Day11
{
    [Theory]
    [InlineData("day11.example.txt", 6, 22)]
    [InlineData("day11.txt", 25, 198089)]
    [InlineData("day11.txt", 75, 236302670835517)]
    public void PartOne(string filePath, int blinkCount, long expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day11.CountStones(fileText, blinkCount).Should().Be(expected);
    }
}