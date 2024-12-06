namespace AoC.CSharp;

public static class Day06
{
    public static int PartOne(string[] fileLines)
    {
        var map = fileLines.Select(c => c.ToCharArray()).ToArray();
        return GetVisitedPositions(map).Count;
    }

    public static int PartTwo(string[] fileLines)
    {
        var map = fileLines.Select(c => c.ToCharArray()).ToArray();
        var loops = 0;

        // for sake of time, brute force this ish by looping over the whole map and placing a blocker in one cell at a time
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] != '.') continue;

                map[i][j] = '#';

                try
                {
                    _ = GetVisitedPositions(map);
                }
                catch // i mean... it works
                {
                    loops++;
                }
                finally
                {
                    map[i][j] = '.';
                }
            }
        }

        return loops;
    }
    
    private static HashSet<Position> GetVisitedPositions(char[][] map)
    {
        var (facing, currentPos) = (Facing.Up, GetStartingPos(map));
        var visitedPositions = Enum.GetValues<Facing>().ToDictionary(x => x, _ => new HashSet<Position>());
        
        while (true)
        {
            if (!visitedPositions[facing].Add(currentPos))
                throw new Exception("in a loop");

            var nextPos = facing switch
            {
                Facing.Up => currentPos with { Row = currentPos.Row - 1 },
                Facing.Right => currentPos with { Col = currentPos.Col + 1 },
                Facing.Down => currentPos with { Row = currentPos.Row + 1 },
                Facing.Left => currentPos with { Col = currentPos.Col - 1 },
                _ => currentPos
            };
            
            if (nextPos.Row < 0 || nextPos.Row >= map.Length || nextPos.Col < 0 || nextPos.Col >= map[0].Length)
                break;
            
            if (map[nextPos.Row][nextPos.Col] == '#')
            {
                facing = facing == Facing.Left ? Facing.Up : facing + 1;
                continue;
            }
            
            currentPos = nextPos;
        }

        return visitedPositions.Values.SelectMany(x => x).ToHashSet();
    }

    private static Position GetStartingPos(char[][] map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '^')
                    return new Position(i, j);
            }
        }

        return new Position(-1, -1);
    }

    private record Position(int Row, int Col);
    private enum Facing
    {
        Up,
        Right,
        Down,
        Left
    }
}