using System.Collections.Immutable;
using AoC.CSharp;
using AoC.CSharp.Common;

namespace AoC.Web.Pages;

public partial class Day16
{
    private MazeModel? Maze { get; set; }
    private MazeRunner? SelectedRunner { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InvokeAsync(() =>
            {
                Maze = CreateMaze();
                StateHasChanged();
            });
        }
    }

    private void Move()
    {
        if (Maze is null) return;

        var runners = Maze.Runners.Where(x => !x.IsDead).ToList();

        foreach (var runner in runners)
        {
            if (runner.Move()) continue;
            
            foreach (var clone in runner.Clones)
                Maze.Runners.Add(clone);

            if (runner.IsDead)
                Maze.Runners.Remove(runner);
        }
    }
    
    private static MazeModel CreateMaze()
    {
        var lines = ExampleInput.Contains("\r\n") ? ExampleInput.Split("\r\n") : ExampleInput.Split("\n");

        var (start, target) = (Vector2D.Zero, Vector2D.Zero);
        var walls = new Dictionary<Vector2D, GridObject>();
        
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y].AsSpan();
            
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '.')
                    continue;

                var position = new Vector2D(x, y);
                
                switch (line[x])
                {
                    case '#': walls.Add(position, new GridObject(position)); break;
                    case 'S': start = position; break;
                    case 'E': target = position; break;
                }
            }
        }

        var maze = new MazeModel
        {
            Height = lines.Length,
            Width = lines[0].Length,
            Target = target,
            Walls = walls.ToImmutableDictionary()
        };

        maze.Runners.Add(maze.CreateRunner(start));

        return maze;
    }

    private void Select(MazeRunner runner)
    {
        if (SelectedRunner == runner)
        {
            SelectedRunner = null;
        }
        else
        {
            SelectedRunner = runner;
        }
    }
    
    private class MazeModel
    {
        public required int Height { get; init; }
        public required int Width { get; init; }
        public required Vector2D Target { get; init; }
        public HashSet<MazeRunner> Runners { get; } = [];
        public HashSet<Vector2D> BadPositions { get; } = [];
        public Dictionary<Vector2D, HashSet<(MazeRunner Runner, Vector2D Direction, int ScoreAtPosition)>> Visited { get; private set; } = [];
        public required ImmutableDictionary<Vector2D, GridObject> Walls { get; init; }

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
                if (BadPositions.Contains(result)) continue;
                //if (Visited.TryGetValue(result, out var visited) && visited.Any(x => x.Facing == possibleDirection)) continue;
                //if (BadPositions.Contains(result)) continue;
                
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

    private class MazeRunner(int id, MazeModel maze, Vector2D position) : GridObject(position)
    {
        private readonly MazeModel _maze = maze;

        public int Id { get; } = id;
        public HashSet<MazeRunner> Clones { get; set; } = [];
        public Vector2D Facing { get; private set; } = Vector2D.Right;
        public int Score { get; private set; }
        public bool IsDead { get; set; }
        public Dictionary<Vector2D, int> Visited { get; set; } = new();

        public IEnumerable<(Vector2D Position, Vector2D Direction)> EnumeratePossibleNextSteps() => _maze
            .EnumerateOpenAdjacentPaths(Position, Facing)
            .Where(x => !Visited.ContainsKey(x.Position));

        public bool Move()
        {
            if (IsDead || Position == _maze.Target)
                return false;

            _ = Visited.TryGetValue(Position, out var score);
            Visited[Position] = score + 1;

            var possibilities = EnumeratePossibleNextSteps()
                .ToDictionary(x => x.Direction, x => x.Position);

            if (possibilities.Count == 0)
            {
                IsDead = true;
                return false;
            }

            if (_maze.Visited.TryGetValue(Position, out var previousStates))
            {
                if (!previousStates.Any(x => x.Direction == Facing))
                {
                    previousStates.Add((this, Facing, Score));
                }
                else
                {
                    var existing = previousStates.First(x => x.Direction == Facing);

                    if (existing.ScoreAtPosition > Score)
                    {
                        existing.Runner.IsDead = true;
                        previousStates.Remove(existing);
                        previousStates.Add((this, Facing, Score));
                    }
                    else
                    {
                        IsDead = true;
                        return false;
                    }
                }
            }
            else
            {
                _maze.Visited[Position] = [(this, Facing, Score)];
            }

            foreach (var (dir, pos) in possibilities.Where(p => p.Key != Facing))
            {
                //var clone = _maze.CreateRunner(pos);
                var clone = CreateClone();
                //clone.Position = pos;
                clone.Facing = dir;
                clone.Score += 1000;
                Clones.Add(clone);
            }
            
            if (possibilities.ContainsKey(Facing))
            {
                Score++;
                Position += Facing;
                return true;
            }

            return false;
        }
        
        private MazeRunner CreateClone()
        {
            var clone = _maze.CreateRunner(Position);
            clone.Facing = Facing;
            clone.Score = Score;
            clone.Visited = Visited.ToDictionary();
            return clone;
        }
    }

    private const string ExampleInput = """
    ###############
    #.......#....E#
    #.#.###.#.###.#
    #.....#.#...#.#
    #.###.#####.#.#
    #.#.#.......#.#
    #.#.#####.###.#
    #...........#.#
    ###.#.#####.#.#
    #...#.....#.#.#
    #.#.#.###.#.#.#
    #.....#...#.#.#
    #.###.#.#.#.#.#
    #S..#.....#...#
    ###############
    """;
}