namespace AoC.CSharp;

public static class Day06MoreOptimized
{
    public static int PartOne(string[] fileLines)
    {
        ReadOnlySpan<string> map = fileLines.AsSpan();
        var pos = GetStartingPos(ref map);
        byte dir = 0;
        
        var visited = new HashSet<int>(map.Length * map[0].Length / 2);

        while (true)
        {
            pos.MoveForward(dir);
            
            if (pos.Row < 0 || pos.Row >= map.Length ||
                pos.Col < 0 || pos.Col >= map[0].AsSpan().Length)
                break;

            if (map[pos.Row].AsSpan()[pos.Col] == '#')
            {
                pos.MoveBackward(dir);
                dir = (byte)((dir + 1) % 4);
                continue;
            }

            visited.Add(pos.GetHashCode());
        }

        return visited.Count + 1;
    }
    
    private static Position GetStartingPos(ref ReadOnlySpan<string> map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            var index = map[i].AsSpan().IndexOf('^');
            
            if (index != -1)
                return new Position(i, index);
        }
        
        throw new Exception("Could not find start pos");
    }
}

public ref struct Position(int row = 0, int col = 0) : IEquatable<Position>
{
    public int Row = row;
    public int Col = col;

    public bool Equals(Position other) => other.Row == Row && other.Col == Col;
    public override int GetHashCode() => HashCode.Combine(Row, Col);

    public void MoveForward(byte dir)
    {
        if (dir == 0) Row--;
        else if (dir == 1) Col++;
        else if (dir == 2) Row++;
        else if (dir == 3) Col--;
    }
    
    public void MoveBackward(byte dir)
    {
        if (dir == 0) Row++;
        else if (dir == 1) Col--;
        else if (dir == 2) Row--;
        else if (dir == 3) Col++;
    }
} 
