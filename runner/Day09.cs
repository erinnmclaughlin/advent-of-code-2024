namespace AoC;

public sealed class Day09
{
    [Theory]
    [InlineData("day09.example.txt", 1928)]
    [InlineData("day09.txt", 6262891638328)]
    public void PartOne(string filePath, long expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day09.PartOne(fileText).Should().Be(expected);
    }

    [Theory]
    [InlineData("day09.example.txt", 2858)]
    [InlineData("day09.txt", 6287317016845)]
    public void PartTwo(string filePath, long expected)
    {
        var fileText = File.ReadAllText(filePath);
        CSharp.Day09.PartTwo(fileText).Should().Be(expected);
    }
}
