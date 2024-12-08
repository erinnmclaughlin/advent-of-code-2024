using Xunit.Abstractions;

namespace AoC;

// an antinode occurs at any point that is perfectly in line with two antennas of the same frequency,
// but only when one of the antennas is twice as far away as the other
public class Day08(ITestOutputHelper output)
{
    private static string[] _fileLines = File.ReadAllLines("day08.txt");

    // 100 is wrong
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
}