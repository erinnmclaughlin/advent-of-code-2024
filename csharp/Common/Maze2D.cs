using System.Diagnostics.CodeAnalysis;

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

    public IEnumerable<(Vector2D Position, Direction Direction)> EnumerateOpenAdjacentPaths(Vector2D position)
    {
        return Directions
            .EnumerateClockwise()
            .Select(x => (Position: position + x, Direction: x))
            .Where(x => IsOpen(x.Position));
    }
}

public class MazeRunnerState2D(Direction direction, Vector2D position)
{
    public int Cost { get; init; }
    public Direction Direction { get; init; } = direction;
    public Vector2D Position { get; init; } = position;
    public MazeRunnerState2D? PreviousState { get; init; }

    public bool Contains(Direction direction, Vector2D position)
    {
        var state = this;

        while (state != null)
        {
            if (state.Direction == direction && state.Position == position)
                return true;

            state = state.PreviousState;
        }

        return false;
    }

    public IEnumerable<Vector2D> EnumerateVisitedPositions()
    {
        var state = this;

        while (state != null)
        {
            yield return state.Position;
            state = state.PreviousState;
        }
    }
    
    public MazeRunnerState2D GetNextState(Direction directionToMove) => new(directionToMove, Position + directionToMove)
    {
        Cost = Cost + (Direction == directionToMove ? 1 : 1001),
        PreviousState = this
    };
}

public class MazeRunner2D
{
    private readonly List<MazeRunnerState2D> _knownBestSolutions = [];
    private readonly PriorityQueue<MazeRunnerState2D, int> _queue = new();
    private readonly HashSet<(Direction Direction, Vector2D Position)> _visited = [];
    
    public Maze2D Maze { get; }
    public Direction StartDirection { get; }
    public Vector2D StartPosition { get; }
    public Vector2D TargetPosition { get; }
    
    public MazeRunner2D(Maze2D maze, Direction startDirection, Vector2D startPosition, Vector2D targetPosition)
    {
        Maze = maze;
        StartDirection = startDirection;
        StartPosition = startPosition;
        TargetPosition = targetPosition;
        
        _queue.Enqueue(new MazeRunnerState2D(startDirection, startPosition), 0);
    }

    public List<MazeRunnerState2D> Solve()
    {
        while (TryMoveNext(out _)) { }

        return _knownBestSolutions;
    }
    
    public bool TryMoveNext([MaybeNullWhen(false)] out MazeRunnerState2D currentState)
    {
        if (!_queue.TryDequeue(out currentState, out _))
            return false;
        
        var firstKnownSolution = _knownBestSolutions.FirstOrDefault();
        var state = (currentState.Direction, currentState.Position);

        if (!_visited.Add(state) && firstKnownSolution?.Contains(currentState.Direction, currentState.Position) is not true) 
            return true;

        if (currentState.Position == TargetPosition)
        {
            if (firstKnownSolution is null)
            {
                _visited.Clear();
                _queue.Clear();
                _queue.Enqueue(new MazeRunnerState2D(StartDirection, StartPosition), 0);
            }
            else if (currentState.Cost > firstKnownSolution.Cost || firstKnownSolution.EnumerateVisitedPositions().SequenceEqual(currentState.EnumerateVisitedPositions()))
            {
                return true;
            }
            
            _knownBestSolutions.Add(currentState);
        }
        else
        {
            foreach (var item in Maze.EnumerateOpenAdjacentPaths(currentState.Position))
            {
                if (item.Direction == currentState.Direction.GetOpposite()) 
                    continue;

                var nextState = currentState.GetNextState(item.Direction);
                _queue.Enqueue(nextState, nextState.Cost);
            }
        }

        return true;
    }
}