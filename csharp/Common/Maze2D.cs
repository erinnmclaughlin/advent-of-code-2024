namespace AoC.CSharp.Common;

public class Maze2D(int height, int width)
{
    public int Height { get; } = height;
    public int Width { get; } = width;

    public HashSet<Vector2D> Walls { get; } = [];

    public bool IsOpen(Vector2D position) => 
        position.X >= 0 && position.X < Width &&
        position.Y >= 0 && position.Y < Height &&
        !Walls.Contains(position);

    public IEnumerable<Vector2D> EnumeratePaths()
    {
        for (var y = 0; y < Height; y++)
        for (var x = 0; x < Width; x++)
        {
            var pos = new Vector2D(x, y);
            
            if (!Walls.Contains(pos))
                yield return pos;
        }
    }
    
    public IEnumerable<(Vector2D Position, Direction Direction)> EnumerateOpenAdjacentPaths(Vector2D position)
    {
        foreach (var direction in Directions.EnumerateClockwise())
        {
            var result = position + direction;

            if (IsOpen(result))
                yield return (result, direction);
        }
    }

    public IEnumerable<Direction> EnumerateOpenDirections(Vector2D position)
    {
        if (!IsOpen(position)) 
            yield break;

        foreach (var direction in Directions.EnumerateClockwise())
        {
            if (IsOpen(position + direction))
                yield return direction;
        }
    }
}