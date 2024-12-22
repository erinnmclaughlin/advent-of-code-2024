namespace AoC.CSharp.Common;

public class MazeRunnerStateContainer
{
    private readonly Dictionary<MazeRunnerState, HashSet<MazeRunnerState>> _alternateRunners = [];
    private readonly List<MazeRunnerState> _runners;
    private readonly Dictionary<Vector2D, HashSet<MazeRunnerStateSnapshot>> _visited;

    public long? LowestTotalCost { get; private set; }
    public Maze2D Maze { get; }
    public Vector2D Start { get; }
    public Vector2D Target { get; }

    public IReadOnlyList<MazeRunnerState> Runners => _runners;

    public MazeRunnerStateContainer(Maze2D maze, MazeRunnerStateSnapshot initialState, Vector2D targetPosition)
    {
        Maze = maze;
        Start = initialState.Position;
        Target = targetPosition;
        
        _runners = [new MazeRunnerState(this, initialState)];
        _visited = Maze.EnumeratePaths().ToDictionary(x => x, _ => new HashSet<MazeRunnerStateSnapshot>());
    }

    public bool HasNext() => _runners.Any(r => r.Position != Target);

    public void MoveNext()
    {
        var deadRunners = new List<MazeRunnerState>();

        foreach (var runner in _runners)
        {
            if (!runner.MoveNext(out var alternateStates) || (LowestTotalCost != null && runner.Cost > LowestTotalCost))
            {
                deadRunners.Add(runner);
            }

            if (runner.Position == Target && (LowestTotalCost == null || runner.Cost < LowestTotalCost))
                LowestTotalCost = runner.Cost;
            
            if (LowestTotalCost != null)
                alternateStates.RemoveAll(x => x.Cost > LowestTotalCost);

            if (_alternateRunners.TryAdd(runner, alternateStates.ToHashSet()))
                continue;
            
            foreach (var alternateState in alternateStates)
                _alternateRunners[runner].Add(alternateState);
        }
        
        deadRunners.ForEach(deadRunner =>
        {
            if (!_alternateRunners.TryGetValue(deadRunner, out var alternateRunners) || alternateRunners.Count == 0)
            {
                _alternateRunners.Remove(deadRunner);

                if (deadRunner.Position != Target)
                    _runners.Remove(deadRunner);

                return;
            }

            var next = alternateRunners.OrderBy(x => x.Cost).First();
            _runners.Add(next);
            _alternateRunners[deadRunner].Remove(next);
        });
    }

    public class MazeRunnerState
    {
        private readonly HashSet<MazeRunnerStateSnapshot> _history;

        public MazeRunnerStateContainer Container { get; }
        public long Cost { get; private set; }
        public Direction Direction { get; private set; }
        public Vector2D Position { get; private set; }

        public IReadOnlySet<MazeRunnerStateSnapshot> History => _history;
        
        public IReadOnlyList<Vector2D> Path => _history
            .Select(x => x.Position)
            .ToList();

        public MazeRunnerState(MazeRunnerStateContainer container, MazeRunnerStateSnapshot initialState)
        {
            Container = container;
            Cost = initialState.Cost;
            Direction = initialState.Direction;
            Position = initialState.Position;

            _history = [initialState];
        }

        public MazeRunnerState(MazeRunnerState currentState, MazeRunnerStateSnapshot nextState)
        {
            Container = currentState.Container;
            Cost = nextState.Cost;
            Direction = nextState.Direction;
            Position = nextState.Position;

            _history = currentState._history.ToHashSet();
            _history.Add(nextState);
        }
        
        public bool MoveNext(out List<MazeRunnerState> alternateStates)
        {
            alternateStates = [];

            if (Position == Container.Target)
                return false;
            
            var nextStates = EnumeratePossibleNextStates().OrderBy(x => x.Cost).ToList();

            if (nextStates.Count == 0)
                return false;

            foreach (var alt in nextStates.Skip(1))
            {
                alternateStates.Add(new MazeRunnerState(this, alt));
            }

            var next = nextStates[0];
            Cost = next.Cost;
            Direction = next.Direction;
            Position = next.Position;
            _history.Add(new MazeRunnerStateSnapshot(this));
            return true;
        }
        
        public IEnumerable<MazeRunnerStateSnapshot> EnumeratePossibleNextStates()
        {
            foreach (var nextDir in Directions.EnumerateClockwise(Direction))
            {
                var nextPos = Position + nextDir;
                
                // don't walk into walls
                if (!Container.Maze.IsOpen(nextPos))
                    continue;

                // don't visit somewhere we've already visited
                if (_history.Any(x => x.Position == nextPos))
                    continue;
                
                // don't walk backwards
                if (nextDir == Direction.GetOpposite())
                    continue;
                
                // don't go down a path that was already traversed in the opposite direction
                // note: this may be an issue if the "backwards" direction is reached before the "forwards" direction?
                // gonna keep it here for now and see if it causes any issues
                if (Container._visited[nextPos].Any(x => x.Direction == nextDir.GetOpposite()))
                    continue;
                
                var cost = Cost + (nextDir == Direction ? 1 : 1001);
                yield return new MazeRunnerStateSnapshot(cost, nextDir, nextPos);
            }
        }
    }
    
    public sealed record MazeRunnerStateSnapshot(long Cost, Direction Direction, Vector2D Position)
    {
        public MazeRunnerStateSnapshot(Direction direction, Vector2D position) : this(0, direction, position){ }
        public MazeRunnerStateSnapshot(MazeRunnerState state) : this(state.Cost, state.Direction, state.Position) { }
    }
}