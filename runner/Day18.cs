using System.Collections.Immutable;
using System.Text;
using AoC.CSharp.Common;
using Xunit.Abstractions;

namespace AoC;

public class Day18(ITestOutputHelper output)
{
    [Theory]
    [InlineData("day18.example.txt", 6, 22)]
    public void PartOne(string inputFile, int size, int expected)
    {
        var target = new Vector2D(size, size);
        var maze = new Maze2D(size + 2, size + 2)
        {
            Walls = File
                .ReadLines(inputFile)
                .Select(l =>
                {
                    var split = l.Split(',');
                    return new Vector2D(int.Parse(split[0]) + 1, int.Parse(split[1]) + 1);
                })
                .Take(12)
                .Concat(Enumerable.Range(0, size + 2).SelectMany(i =>
                {
                    return new[]
                    {
                        new Vector2D(i, 0),
                        new Vector2D(i, size + 2),
                        new Vector2D(0, i),
                        new Vector2D(size + 2, i),
                    };
                }))
                .Distinct()
                .ToHashSet()
        };

        var sb = new StringBuilder();
        
        for (var y = 1; y < maze.Height; y++)
        {
            for (var x = 1; x < maze.Width; x++)
            {
                if (maze.Walls.Contains(new Vector2D(x, y)))
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }
            }

            sb.AppendLine();
        }
        
        output.WriteLine(sb.ToString());
        
    }
}