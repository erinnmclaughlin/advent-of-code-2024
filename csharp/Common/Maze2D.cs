using System.Text;

namespace AoC.CSharp.Common;

public class Maze2D(int height, int width)
{
    public int Height { get; } = height;
    public int Width { get; } = width;

    public HashSet<Rectangle2D> Walls { get; init; } = [];

    public static Maze2D Parse(ReadOnlySpan<string> input, char wallChar = '#')
    {
        var maze = new Maze2D(input.Length, input[0].Length);

        // vertical walls
        for (var x = 0; x < maze.Width; x++)
        {
            var start = -1;
            var height = 0;

            for (var y = 0; y < maze.Height; y++)
            {
                if (input[y][x] != wallChar)
                {
                    if (start == -1) continue;

                    maze.Walls.Add(Rectangle2D.Create(x, start, height, 1));
                    start = -1;
                    height = 0;
                    continue;
                }

                if (start == -1)
                    start = y;

                height++;
            }

            if (start != -1)
            {
                maze.Walls.Add(Rectangle2D.Create(x, start, height, 1));
            }
        }

        return maze;
    }

    public string GetString(char pathChar = '.', char wallChar = '#')
    {
        var sb = new StringBuilder();

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                sb.Append(Walls.Any(w => w.Contains(x, y)) ? wallChar : pathChar);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public IEnumerable<(Vector2D Position, Direction Direction)> EnumerateOpenAdjacentPaths(Vector2D position)
    {
        foreach (var possibleDirection in Directions.EnumerateClockwise())
        {
            var result = position + possibleDirection;

            if (result.X < 0 || result.X >= Width) continue;
            if (result.Y < 0 || result.Y >= Height) continue;
            if (Walls.Any(w => w.Contains(result))) continue;
            
            yield return (result, possibleDirection);
        }
    }
}