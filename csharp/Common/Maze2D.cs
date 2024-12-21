namespace AoC.CSharp.Common;

public class Maze2D(int height, int width)
{
    public int Height { get; } = height;
    public int Width { get; } = width;

    public HashSet<Vector2D> Walls { get; init; } = [];
    
    public IEnumerable<(Vector2D Position, Vector2D Direction)> EnumerateOpenAdjacentPaths(Vector2D position)
    {
        foreach (var possibleDirection in EnumerateDirections())
        {
            var result = position + possibleDirection;

            if (result.X < 0 || result.X >= Width) continue;
            if (result.Y < 0 || result.Y >= Height) continue;
                
            if (!Walls.Contains(result))
                yield return (result, possibleDirection);
        }
    }

    private static IEnumerable<Vector2D> EnumerateDirections()
    {
        yield return Vector2D.Up;
        yield return Vector2D.Right;
        yield return Vector2D.Down;
        yield return Vector2D.Left;
    }
}