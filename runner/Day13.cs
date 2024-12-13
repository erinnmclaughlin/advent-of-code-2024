namespace AoC;

public class Day13
{
    private readonly string[] _fileLines = File.ReadAllLines("day13.txt");

    [Fact]
    public void PartOne()
    {
        var sum = CSharp.Day13.PartOne(_fileLines);
        Assert.Equal(36954, sum);
    }

    [Fact]
    public void PartTwo()
    {
        var sum = CSharp.Day13.PartTwo(_fileLines);
        Assert.Equal(79352015273424, sum);
    }
}