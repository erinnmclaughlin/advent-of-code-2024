namespace AoC.CSharp;

public static class Day08
{
    public static int PartOne(string[] fileLines)
    {
        var grid = fileLines
            .Select((l, i) => l.Select((c, j) => (Row: i, Col: j, Type: c)).ToArray())
            .ToArray();

        var antennaMap = grid
            .SelectMany(x => x)
            .GroupBy(x => x.Type)
            .Where(x => x.Key != '.')
            .ToDictionary(x => x.Key, x => x.ToArray());

        var antinodes = new HashSet<(int, int)>();

        foreach (var (_, antennae) in antennaMap)
        {
            for (var i = 0; i < antennae.Length; i++)
            {
                for (var j = i + 1; j < antennae.Length; j++)
                {
                    var (dX, dY) = (antennae[j].Col - antennae[i].Col, antennae[j].Row - antennae[i].Row);
                    
                    var c1 = (antennae[i].Row - dY, antennae[i].Col - dX);
                    var c2 = (antennae[j].Row + dY, antennae[j].Col + dX);
                    
                    if (grid.IsValidPosition(c1))
                    {
                        antinodes.Add(c1);
                    }
                        
                    if (grid.IsValidPosition(c2))
                    {
                        antinodes.Add(c2);
                    }
                }
            }
        }

        return antinodes.Count;
    }
    
    public static int PartTwo(string[] fileLines)
    {
        var antinodes = new HashSet<(int, int)>();
        
        var grid = fileLines
            .Select((l, i) => l.Select((c, j) => (Row: i, Col: j, Type: c)).ToArray())
            .ToArray();

        var antennaMap = grid
            .SelectMany(x => x)
            .GroupBy(x => x.Type)
            .Where(x => x.Key != '.')
            .ToDictionary(x => x.Key, x => x.ToArray());

        foreach (var (_, antennae) in antennaMap)
        {
            for (var i = 0; i < antennae.Length; i++)
            {
                for (var j = i + 1; j < antennae.Length; j++)
                {
                    var c1 = (antennae[i].Row, antennae[i].Col);
                    var c2 = (antennae[j].Row, antennae[j].Col);
                    
                    var (dX, dY) = (c2.Col - c1.Col, c2.Row - c1.Row);

                    while (grid.IsValidPosition(c1))
                    {
                        antinodes.Add(c1);
                        c1 = (c1.Row - dY, c1.Col - dX);
                    }

                    while (grid.IsValidPosition(c2))
                    {
                        antinodes.Add(c2);
                        c2 = (c2.Row + dY, c2.Col + dX);
                    }
                }
            }
        }

        return antinodes.Count;
    }

}