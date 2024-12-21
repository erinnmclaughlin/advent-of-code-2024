using System.Diagnostics;

namespace AoC.CSharp.Common;

public static class Directions
{
    public static IEnumerable<Direction> EnumerateClockwise() => EnumerateClockwise(Direction.Up);
    
    public static IEnumerable<Direction> EnumerateClockwise(Direction startingDirection)
    {
        var dir = startingDirection;
        yield return dir;
        
        for (var i = 0; i < 3; i++)
        {
            yield return dir = dir switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new UnreachableException()
            };
        }
    }
}