using System.Collections.Immutable;
using AoC.CSharp.Common;
using Xunit.Abstractions;

namespace AoC;

public class Day16(ITestOutputHelper output)
{
    //private readonly string[] _fileLines = File.ReadAllLines("day16.txt");

    // 155528 is too high
    // 155528
    [Theory]
    [InlineData("day16.example1.txt", 7036)]
    [InlineData("day16.example2.txt", 11048)]
    [InlineData("day16.txt", 0)]
    public void PartOne(string path, int expected)
    {
        var maze = CreateMaze(File.ReadAllLines(path));

        while (true)
        {
            foreach (var group in maze.Runners.GroupBy(x => (x.Position, x.Facing)))
            foreach (var runner in group.OrderBy(x => x.Score).Skip(1))
                runner.IsDead = true;
        
            maze.Runners.RemoveWhere(x => x.IsDead);
        
            var runners = maze.Runners.ToList();

            foreach (var runner in runners)
                runner.Move();
        
            maze.Runners.RemoveWhere(x => x.IsDead);
            
            if (maze.Runners.All(x => x.Position == maze.Target))
                break;
            /*
            maze.Runners.RemoveWhere(x => x.IsDead);
            
            if (maze.Runners.All(x => x.Position == maze.Target))
                break;
            
            var runners = maze.Runners.ToList();

            foreach (var runner in runners)
                runner.Move();*/
        }

        foreach (var runner in maze.Runners)
        {
            output.WriteLine("Runner {0}: Score: {1}", runner.Id, runner.Score);
        }
        
        Assert.Equal(expected, maze.Runners.Min(x => x.Score));
    }
    
    private static MazeModel CreateMaze(ReadOnlySpan<string> lines)
    {
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

        _ = maze.SpawnRunner(start);

        return maze;
    }
    
    private class MazeModel
    {
        public required int Height { get; init; }
        public required int Width { get; init; }
        public required Vector2D Target { get; init; }
        public HashSet<MazeRunner> Runners { get; } = [];
        public HashSet<Vector2D> BadPositions { get; } = [];
        public HashSet<(Vector2D Position, Vector2D Facing)> Visited { get; private set; } = [];
        public required ImmutableDictionary<Vector2D, GridObject> Walls { get; init; }

        public MazeRunner SpawnRunner(Vector2D position)
        {
            var runner = new MazeRunner(Runners.Count + 1, this, position);
            Runners.Add(runner);
            return runner;
        }
        
        public IEnumerable<Vector2D> EnumerateAdjacentPaths(Vector2D position)
        {
            return EnumerateAllAdjacentDirections(position).Where(p => !BadPositions.Contains(p) && !Walls.ContainsKey(p));
        }

        private static IEnumerable<Vector2D> EnumerateAllAdjacentDirections(Vector2D position)
        {
            yield return position + Vector2D.Up;
            yield return position + Vector2D.Right;
            yield return position + Vector2D.Down;
            yield return position + Vector2D.Left;
        }
    }

    private class MazeRunner(int id, MazeModel maze, Vector2D position) : GridObject(position)
    {
        private readonly MazeModel _maze = maze;

        public int Id { get; } = id;
        public Vector2D Facing { get; private set; } = Vector2D.Right;
        public int Score { get; private set; }
        public bool IsDead { get; set; }
        //public HashSet<Vector2D> Visited { get; set; } = [];

        public IEnumerable<Vector2D> EnumeratePossibleNextSteps() => _maze
            .EnumerateAdjacentPaths(Position)
            .Where(position => !_maze.Visited.Contains((position, Position - position)));

        public void Move()
        {
            if (IsDead || Position == _maze.Target)
                return;
            
            //Visited.Add(Position);

            var possibilities = EnumeratePossibleNextSteps().ToHashSet();

            if (possibilities.Count == 0 || !_maze.Visited.Add((Position, Facing)))
            {
                IsDead = true;
                return;
            }

            foreach (var other in possibilities.Where(p => p != Position + Facing))
            {
                var clone = _maze.SpawnRunner(Position);
                clone.Facing = other - Position;
                clone.Score = Score + 1000;
                //clone.Visited = Visited.ToHashSet();
            }
            
            if (possibilities.Contains(Position + Facing))
            {
                Score++;
                Position += Facing;
            }
        }
    }
}