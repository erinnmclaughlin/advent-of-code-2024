namespace AoC.CSharp.Common;

public class Maze2D(int height, int width)
{
    public int Height { get; } = height;
    public int Width { get; } = width;

    public HashSet<Vector2D> Walls { get; init; } = [];

    public bool IsOpen(Vector2D position) => 
        position.X >= 0 && position.X < Width &&
        position.Y >= 0 && position.Y < Height &&
        !Walls.Contains(position);
    
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

    public IEnumerable<Vector2D> EnumerateNextDecisionPoints(Vector2D position)
    {
        foreach (var direction in EnumerateOpenDirections(position))
        {
            var currentPosition = position;
            var nextPosition = currentPosition + direction;

            while (IsOpen(nextPosition) && EnumerateOpenDirections(nextPosition).Count() > 2)
            {
                currentPosition = nextPosition;
                nextPosition += direction;
            }

            if (currentPosition != position)
                yield return currentPosition;
        }
    }
}