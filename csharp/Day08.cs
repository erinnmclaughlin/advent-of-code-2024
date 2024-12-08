namespace AoC.CSharp;

using Cell = (int Row, int Col);

public static class Day08
{
    public static int PartOne(string[] fileLines) => GetAntennaPairs(fileLines)
        .Aggregate(new HashSet<Cell>(), (agg, x) =>
        {
            var (c1, c2) = x;
            var (dX, dY) = (c2.Col - c1.Col, c2.Row - c1.Row);
            
            c1 = (c1.Row - dY, c1.Col - dX);
            if (fileLines.IsInBounds(c1)) 
                agg.Add(c1);

            c2 = (c2.Row + dY, c2.Col + dX);
            if (fileLines.IsInBounds(c2)) 
                agg.Add(c2);

            return agg;
        })
        .Count;

    public static int PartTwo(string[] fileLines) => GetAntennaPairs(fileLines)
        .Aggregate(new HashSet<Cell>(), (agg, x) =>
        {
            var (c1, c2) = x;
            var (dX, dY) = (c2.Col - c1.Col, c2.Row - c1.Row);

            while (fileLines.IsInBounds(c1))
            {
                agg.Add(c1);
                c1 = (c1.Row - dY, c1.Col - dX);
            }

            while (fileLines.IsInBounds(c2))
            {
                agg.Add(c2);
                c2 = (c2.Row + dY, c2.Col + dX);
            }

            return agg;
        })
        .Count;

    private static IEnumerable<(Cell c1, Cell c2)> GetAntennaPairs(string[] grid)
    {
        var antennaMap = grid
            .SelectMany((l, i) => l.Select((c, j) => new { Cell = new Cell(i, j), Type = c }).ToArray())
            .Where(x => x.Type != '.')
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, g => g.Select(x => x.Cell).ToArray());

        foreach (var (_, antennae) in antennaMap)
            for (var i = 0; i < antennae.Length; i++)
            for (var j = i + 1; j < antennae.Length; j++)
                yield return ((antennae[i].Row, antennae[i].Col), (antennae[j].Row, antennae[j].Col));
    }
    
    private static bool IsInBounds(this string[] grid, Cell cell) =>
        cell.Row >= 0 && 
        cell.Row < grid.Length &&
        cell.Col >= 0 && 
        cell.Col < grid[0].Length;
}