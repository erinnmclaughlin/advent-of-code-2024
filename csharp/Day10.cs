namespace AoC.CSharp;
using Coord = (int Row, int Col);

public static class Day10
{
    public static int PartOne(string[] lines) => lines
        .EnumerateStartingCoords()
        .SelectMany(x => lines.EnumeratePathEnds(x).Distinct())
        .Count();

    public static int PartTwo(string[] lines) => lines
        .EnumerateStartingCoords()
        .SelectMany(lines.EnumeratePathEnds)
        .Count();
    
    private static IEnumerable<Coord> EnumerateStartingCoords(this string[] lines)
    {
        for (var r = 0; r < lines.Length; r++)
        for (var c = 0; c < lines[r].Length; c++)
            if (lines[r][c] == '0')
                yield return new Coord(r, c);
    }
    
    private static IEnumerable<Coord> EnumeratePathEnds(this string[] lines, Coord pos)
    {
        if (lines[pos.Row][pos.Col] == '9')
            yield return pos;

        foreach (var next in EnumerateNextSteps(lines, pos))
        foreach (var nextEnd in EnumeratePathEnds(lines, next))
            yield return nextEnd;
    }

    private static IEnumerable<Coord> EnumerateNextSteps(string[] map, Coord pos)
    {
        var nextValue = map[pos.Row][pos.Col] + 1;
        
        if (pos.Row > 0 && map[pos.Row - 1][pos.Col] == nextValue)
            yield return new Coord(pos.Row - 1, pos.Col);
        
        if (pos.Row < map.Length - 1 && map[pos.Row + 1][pos.Col] == nextValue)
            yield return new Coord(pos.Row + 1, pos.Col);
        
        if (pos.Col > 0 && map[pos.Row][pos.Col - 1] == nextValue)
            yield return new Coord(pos.Row, pos.Col - 1);
        
        if (pos.Col < map[0].Length - 1 && map[pos.Row][pos.Col + 1] == nextValue)
            yield return new Coord(pos.Row, pos.Col + 1);
    }
}