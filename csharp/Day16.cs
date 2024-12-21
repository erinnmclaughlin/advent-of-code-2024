namespace AoC.CSharp;

public static class Day16
{
    public class MazeModel
    {
        public required Maze2D Maze { get; init; }
        public Vector2D Target { get; set; }
        public HashSet<MazeRunner> Runners { get; init; } = [];
        public Dictionary<Vector2D, HashSet<MazeRunner>> Visited { get; private set; } = [];

        public static MazeModel Parse(ReadOnlySpan<string> text)
        {
            var model = new MazeModel
            {
                Maze = new Maze2D(text.Length, text[0].Length)
            };
            
            for (var y = 0; y < text.Length; y++)
            {
                var line = text[y].AsSpan();
            
                for (var x = 0; x < line.Length; x++)
                {
                    var position = new Vector2D(x, y);

                    switch (line[x])
                    {
                        case '#': 
                            model.Maze.Walls.Add(Rectangle2D.Create(position, 1, 1));
                            break;
                        case 'S':
                            model.Runners.Add(model.CreateRunner(position));
                            model.Visited.Add(position, []); 
                            break;
                        case 'E': 
                            model.Target = position; 
                            model.Visited.Add(position, []);  
                            break;
                        default: 
                            model.Visited.Add(position, []); 
                            break;
                    }
                }
            }

            return model;
        }
        
        public MazeRunner CreateRunner(Vector2D position)
        {
            return new MazeRunner(Runners.Count + 1, this, position);
        }
        
    }

    public class MazeRunner(int id, MazeModel maze, Vector2D position)
    {
        private readonly MazeModel _maze = maze;

        public int Id { get; } = id;
        public HashSet<MazeRunner> Clones { get; set; } = [];
        public Vector2D Direction { get; set; } = Vector2D.Right;
        public Vector2D Position { get; set; } = position;
        public int Score { get; private set; }
        public bool IsDead { get; set; }
        public Dictionary<Vector2D, HashSet<Vector2D>> Visited { get; set; } = new();

        public IEnumerable<(Vector2D Position, Vector2D Direction)> EnumeratePossibleNextSteps() => _maze
            .Maze
            .EnumerateOpenAdjacentPaths(Position)
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