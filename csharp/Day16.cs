using System.Collections.Immutable;

namespace AoC.CSharp;

public static class Day16
{
    public class MazeModel
    {
        public required int Height { get; init; }
        public required int Width { get; init; }
        public required Vector2D Target { get; init; }
        public HashSet<MazeRunner> Runners { get; } = [];
        //public HashSet<Vector2D> BadPositions { get; } = [];
        public Dictionary<Vector2D, HashSet<MazeRunner>> Visited { get; private set; } = [];
        public required ImmutableDictionary<Vector2D, GridObject> Walls { get; init; }

        public static MazeModel Parse(ReadOnlySpan<string> text)
        {
            var (start, target) = (Vector2D.Zero, Vector2D.Zero);
            var paths = new HashSet<Vector2D>();
            var walls = new Dictionary<Vector2D, GridObject>();
        
            for (var y = 0; y < text.Length; y++)
            {
                var line = text[y].AsSpan();
            
                for (var x = 0; x < line.Length; x++)
                {
                    var position = new Vector2D(x, y);

                    switch (line[x])
                    {
                        case '#': walls.Add(position, new GridObject(position)); break;
                        case 'S': start = position; paths.Add(position); break;
                        case 'E': target = position; paths.Add(position); break;
                        default: paths.Add(position); break;
                    }
                }
            }

            var maze = new MazeModel
            {
                Height = text.Length,
                Width = text[0].Length,
                Target = target,
                Walls = walls.ToImmutableDictionary()
            };

            maze.Runners.Add(maze.CreateRunner(start));
            maze.Visited = paths.ToDictionary(x => x, _ => new HashSet<MazeRunner>());

            return maze;
        }
        
        public MazeRunner CreateRunner(Vector2D position)
        {
            return new MazeRunner(Runners.Count + 1, this, position);
        }
        
        public IEnumerable<(Vector2D Position, Vector2D Direction)> EnumerateOpenAdjacentPaths(Vector2D position, Vector2D dir)
        {
            foreach (var possibleDirection in EnumerateDirections(dir))
            {
                var result = position + possibleDirection;

                if (result.X < 0 || result.X >= Width) continue;
                if (result.Y < 0 || result.Y >= Height) continue;
                
                if (Walls.ContainsKey(result)) continue;
                //if (BadPositions.Contains(result)) continue;
                //if (Visited.TryGetValue(result, out var visited) && visited.Any(x => x.Facing == possibleDirection)) continue;
                
                yield return (result, possibleDirection);
            }
        }

        private static IEnumerable<Vector2D> EnumerateDirections(Vector2D dir)
        {
            yield return dir;
            yield return new Vector2D(dir.Y, -dir.X);
            yield return new Vector2D(-dir.Y, dir.X);
        }
    }

    public class MazeRunner(int id, MazeModel maze, Vector2D position) : GridObject(position)
    {
        private readonly MazeModel _maze = maze;

        public int Id { get; } = id;
        public HashSet<MazeRunner> Clones { get; set; } = [];
        public Vector2D Direction { get; private set; } = Vector2D.Right;
        public int Score { get; private set; }
        public bool IsDead { get; set; }
        public Dictionary<Vector2D, HashSet<Vector2D>> Visited { get; set; } = new();

        public IEnumerable<(Vector2D Position, Vector2D Direction)> EnumeratePossibleNextSteps() => _maze
            .EnumerateOpenAdjacentPaths(Position, Direction)
            .Where(x => !Visited.ContainsKey(x.Position));

        public bool Move()
        {
            if (IsDead || Position == _maze.Target)
                return false;

            _ = Visited.TryAdd(Position, []);
            Visited[Position].Add(Direction);

            if (!_maze.Visited[Position].Add(this))
            {
                IsDead = true;
                return false;
            }

            if (_maze.Visited[Position] is { Count: > 0 } runnersWithSharedPosition)
            {
                var current = this;
                var possibleDuplicates = runnersWithSharedPosition.Where(x => x != current && x.Visited[Position].Contains(Direction));
                
                var isDuplicate = false;

                foreach (var possibleDuplicate in possibleDuplicates)
                {
                    if (Visited.All(x => possibleDuplicate.Visited.Contains(x)))
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (isDuplicate)
                {
                    Clones.Clear();
                    IsDead = true;
                    return false;
                }
            }

            var possibilities = EnumeratePossibleNextSteps()
                .ToDictionary(x => x.Direction, x => x.Position);

            if (possibilities.Count == 0)
            {
                IsDead = true;
                return false;
            }

            foreach (var (dir, pos) in possibilities.Where(p => p.Key != Direction))
            {
                //var clone = _maze.CreateRunner(pos);
                var clone = CreateClone();
                clone.Position = pos;
                clone.Direction = dir;
                clone.Score += 1001;
                Clones.Add(clone);
            }
            
            if (possibilities.ContainsKey(Direction))
            {
                Score++;
                Position += Direction;
                return true;
            }

            return false;
        }
        
        private MazeRunner CreateClone()
        {
            var clone = _maze.CreateRunner(Position);
            clone.Direction = Direction;
            clone.Score = Score;
            clone.Visited = Visited.ToDictionary();
            return clone;
        }
    }
}