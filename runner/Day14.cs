using AoC.CSharp;

namespace AoC;

public class Day14
{
    private readonly string[] _input = File.ReadAllLines("day14.txt");
    
    [Fact]
    public void PartOne()
    {
        var score = _input.Select(l =>
        {
            var robot = CSharp.Day14.Robot.Parse(l);
            robot.Move(100, 103, 101);
            return robot;
        })
        .GetQuadrantScore(103, 101);
        
        Assert.Equal(216772608, score);
    }
    
}