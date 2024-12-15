namespace AoC;

public class Day15
{
    private readonly string[] _fileLines = File.ReadAllLines("day15.txt");

    [Fact]
    public void PartOne()
    {
        var span = _fileLines.AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = CSharp.Day15.CreatePartOneWorld(span[..splitIndex]);

        for (var i = splitIndex; i < _fileLines.Length; i++)
        {
            foreach (var dir in span[i].Select(CSharp.Day15.ParseInstruction))
            {
                world.TryMove(world.Robot, dir);
            }
        }
        
        Assert.Equal(1438161, world.GameObjects.OfType<CSharp.Day15.Box>().Sum(world.GetGpsLocation));
    }

    [Fact]
    public void PartTwo()
    {
        var span = _fileLines.AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = CSharp.Day15.CreatePartTwoWorld(span[..splitIndex]);
        
        for (var i = splitIndex; i < span.Length; i++)
        {
            foreach (var dir in span[i].Select(CSharp.Day15.ParseInstruction))
            {
                if (world.CanMove(world.Robot, dir))
                    world.TryMove(world.Robot, dir);
            }
        }

        Assert.Equal(1437981, world.GameObjects.OfType<CSharp.Day15.Box>().Sum(world.GetGpsLocation));
    }
}