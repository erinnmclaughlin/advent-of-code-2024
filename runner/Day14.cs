namespace AoC;

public class Day14
{
    private readonly string[] _input = File.ReadAllLines("day14.txt");
    
    [Fact]
    public void PartOne() => Assert.Equal(216772608, CSharp.Day14.PartOne(_input));
}
