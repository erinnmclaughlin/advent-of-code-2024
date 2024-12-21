namespace AoC;

public sealed class Day15
{
    [Theory]
    [InlineData("day15.example1.txt", 2028)]
    [InlineData("day15.example2.txt", 10092)]
    [InlineData("day15.txt", 1438161)]
    public void PartOne(string filePath, int expected)
    {
        var span = File.ReadAllLines(filePath).AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = CSharp.Day15.CreatePartOneWorld(span[..splitIndex]);

        for (var i = splitIndex; i < span.Length; i++)
        {
            foreach (var dir in span[i].Select(CSharp.Day15.ParseInstruction))
            {
                world.TryMove(world.Robot, dir);
            }
        }

        world.MapObjects
            .OfType<CSharp.Day15.Box>()
            .Sum(CSharp.Day15.World.GetGpsLocation)
            .Should()
            .Be(expected);
    }

    [Theory]
    [InlineData("day15.example2.txt", 9021)]
    [InlineData("day15.txt", 1437981)]
    public void PartTwo(string filePath, int expected)
    {
        var span = File.ReadAllLines(filePath).AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = CSharp.Day15.CreatePartTwoWorld(span[..splitIndex]);
        
        for (var i = splitIndex; i < span.Length; i++)
        {
            foreach (var dir in span[i].Select(CSharp.Day15.ParseInstruction))
            {
                world.TryMove(world.Robot, dir);
            }
        }

        world.MapObjects
            .OfType<CSharp.Day15.Box>()
            .Sum(CSharp.Day15.World.GetGpsLocation)
            .Should()
            .Be(expected);
    }
}