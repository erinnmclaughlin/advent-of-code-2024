namespace AoC;

public class Day11
{
    private readonly string _fileText = File.ReadAllText("day11.txt");

    [Fact]
    public void PartOne() => Assert.Equal(198089, CSharp.Day11.CountStones(_fileText, 25));

    [Fact]
    public void PartTwo() => Assert.Equal(236302670835517, CSharp.Day11.CountStones(_fileText, 75));
}