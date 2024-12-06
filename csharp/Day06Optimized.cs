using System.Diagnostics;

namespace AoC.CSharp;

public static class Day06Optimized
{
    public static int PartOne(Span<string> map)
    {
        var (x, y) = GetStartingPos(ref map);
        var facing = Facing.Up;

        var (nextX, nextY) = (x, y);
        var visited = new HashSet<(int, int)> { (nextX,nextY) };

        while (true)
        {
            (nextX, nextY) = facing switch
            {
                Facing.Up =>    (x - 1, y    ),
                Facing.Right => (x    , y + 1),
                Facing.Down =>  (x + 1, y    ),
                Facing.Left =>  (x,     y - 1),
                _ => throw new UnreachableException()
            };
            
            if (nextX < 0 || nextX >= map.Length || nextY < 0 || nextY >= map[0].AsSpan().Length)
                break;

            if (map[nextX].AsSpan()[nextY] == '#')
            {
                facing = facing == Facing.Left ? Facing.Up : facing + 1;
                continue;
            }

            visited.Add((nextX,nextY));
            (x, y) = (nextX, nextY);
        }

        return visited.Count;
    }

    private static (int, int) GetStartingPos(ref Span<string> map)
    { 
        for (var i = 0; i < map.Length; i++)
        {
            var index = map[i].IndexOf('^');
            
            if (index != -1)
                return (i, index);
        }
        
        throw new Exception("Could not find start pos");
    }
    
    private enum Facing : byte
    {
        Up,
        Right,
        Down,
        Left
    }
}