using Xunit.Abstractions;

namespace AoC;

public class Day08(ITestOutputHelper output)
{
    private static readonly string[] _fileLines = File.ReadAllLines("day08.txt");

    [Fact]
    public void PartOne()
    {
        var grid = _fileLines.Select(x => x.ToArray()).ToArray();
        
        var antennae = grid
            .SelectMany(x => x)
            .Where(x => x != '.')
            .Distinct()
            .ToArray();

        var antinodes = new HashSet<(int Row, int Col)>();

        foreach (var a in antennae)
        {
            var coords = new List<(int X, int Y)>();
            
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == a)
                        coords.Add((i, j));
                }
            }

            for (var i = 0; i < coords.Count; i++)
            {
                var coord1 = coords[i];

                for (var j = i + 1; j < coords.Count; j++)
                {
                    var coord2 = coords[j];

                    var rowDist = coord2.X - coord1.X;
                    var colDist = coord2.Y - coord1.Y;
                    
                    //output.WriteLine("{0}{1}", rowDist, colDist);

                    if (coord1.X == coord2.X)
                        throw new Exception("same line");

                    
                    antinodes.Add((coord1.X - rowDist, coord1.Y - colDist));
                    antinodes.Add((coord2.X + rowDist, coord2.Y + colDist));
                }
            }
        }

        for (var i = antinodes.Count - 1; i >= 0; i--)
        {
            var (row,col) = antinodes.ElementAt(i);
            if (row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length)
            {
                antinodes.Remove((row,col));
                continue;
            }

            /*if (grid[row][col] != '.')
            {
                antinodes.Remove((row,col));
                continue;
            }*/

            grid[row][col] = '#';
        }

        output.WriteLine(antinodes.Count.ToString());
        
        foreach (var line in grid)
        {
            output.WriteLine(string.Join("", line));
        }
    }

    // 973 = nope
    [Fact]
    public void PartTwo()
    {
        var grid = new GridCell[_fileLines.Length][];
        
        for (var i = 0; i < _fileLines.Length; i++)
        {
            grid[i] = new GridCell[_fileLines[i].Length];
            
            for (var j = 0; j < _fileLines[i].Length; j++)
            {
                grid[i][j] = new GridCell(i, j, _fileLines[i][j]);
            }
        }

        var antennaMap = grid
            .SelectMany(x => x)
            .Where(x => x.IsAntenna)
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        foreach (var (_, antennae) in antennaMap)
        {
            for (var i = 0; i < antennae.Count; i++)
            {
                for (var j = i + 1; j < antennae.Count; j++)
                {
                    var rowDiff = antennae[j].Row - antennae[i].Row;
                    var colDiff = antennae[j].Col - antennae[i].Col;

                    var cursor = (antennae[i].Row, antennae[i].Col);

                    var nextRow = cursor.Row - rowDiff;
                    var nextCol = cursor.Col - colDiff;

                    while (nextRow >= 0 && nextCol >= 0 && nextCol < grid[0].Length)
                    {
                        cursor = (nextRow, nextCol);
                        nextRow = cursor.Row - rowDiff;
                        nextCol = cursor.Col - colDiff;
                    }

                    while (cursor.Row < grid.Length && cursor.Col >= 0 && cursor.Col < grid[0].Length)
                    {
                        grid[cursor.Row][cursor.Col].IsAntinode = true;
                        cursor = (cursor.Row + rowDiff, cursor.Col + colDiff);
                    }
                }
            }
        }

        output.WriteLine(grid.Sum(r => r.Count(g => g.IsAntinode)).ToString());
        
        foreach (var row in grid)
        {
            output.WriteLine(string.Join("", row.Select(r => r.IsAntinode && !r.IsAntenna ? '#' : r.Type)));
        }
    }

    private record GridCell(int Row, int Col, char Type)
    {
        public bool IsAntenna => Type != '.';
        public bool IsAntinode { get; set; }
    }
}