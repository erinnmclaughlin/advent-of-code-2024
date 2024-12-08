namespace AoC.CSharp;
/*
public static class Day08
{
    public static int PartTwo(string[] fileLines)
    {
        var grid = fileLines
            .Select((l, i) => l.Select((c, j) => new GridCell(i, j, c)).ToArray())
            .ToArray();

        var antennaMap = grid
            .SelectMany(x => x)
            .GroupBy(x => x.Type)
            .Where(x => x.Key != '.')
            .ToDictionary(x => x.Key, x => x.ToArray());

        foreach (var (type, antennae) in antennaMap)
        {
            for (var i = 0; i < antennae.Length; i++)
            {
                
            }
        }
    }

    private record GridCell(int Row, int Col, char Type)
    {
        public bool IsAntenna => Type != '.';
        public bool IsAntinode { get; set; }
    }
}*/