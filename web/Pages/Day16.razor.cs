using System.Collections.Immutable;
using AoC.CSharp.Common;

namespace AoC.Web.Pages;

public partial class Day16
{
    private MazeRunner? Runner { get; set; }
    private MazeRunner? SecondaryRunner { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InvokeAsync(() =>
            {
                Runner = CreateRunner();
                StateHasChanged();
            });
        }
    }

    private static MazeRunner CreateRunner()
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

        var maze = new Maze
        {
            Height = lines.Length,
            Width = lines[0].Length,
            Walls = walls.ToImmutableDictionary()
        };

        return new MazeRunner(maze, start, target);
    }
    
    private class Maze
    {
        public required int Height { get; init; }
        public required int Width { get; init; }
        public required ImmutableDictionary<Vector2D, GridObject> Walls { get; init; }

        public IEnumerable<Vector2D> EnumerateAdjacentPaths(Vector2D position)
        {
            if (!Walls.ContainsKey(position + Vector2D.Up))
                yield return position + Vector2D.Up;

            if (!Walls.ContainsKey(position + Vector2D.Down))
                yield return position + Vector2D.Down;

            if (!Walls.ContainsKey(position + Vector2D.Left))
                yield return position + Vector2D.Left;

            if (!Walls.ContainsKey(position + Vector2D.Right))
                yield return position + Vector2D.Right;
        }
    }

    private class MazeRunner(Maze maze, Vector2D start, Vector2D target) : GridObject(start)
    {
        public MazeRunner? CurrentChildRunner { get; set; }
        
        public Maze Maze { get; } = maze;
        public Vector2D Target { get; } = target;
        public Vector2D Facing { get; set; } = Vector2D.Right;
        public Stack<Vector2D> DecisionPoints { get; set; } = new();
        public HashSet<Vector2D> BadPositions { get; set; } = [];
        public Stack<Vector2D> Visited { get; set; } = [];

        public bool IsBlocked => Maze.EnumerateAdjacentPaths(Position).All(x => x == Position - Facing);
        public bool IsBacktracking { get; private set; }
        public bool IsUnwinding { get; private set; }
        public bool IsResetting { get; private set; }

        public IEnumerable<Vector2D> EnumeratePossibleNextSteps() => Maze
            .EnumerateAdjacentPaths(Position)
            .Where(x => !BadPositions.Contains(x) && !Visited.Contains(x) && x != Position - Facing);
        
        public void Move()
        {
            if (CurrentChildRunner != null)
            {
                CurrentChildRunner.Move();
                return;
            }

            if (Position == Target)
            {
                CurrentChildRunner = CreateSecondaryRunner();
                return;
            }
            
            if (IsBacktracking || IsUnwinding || IsResetting)
            {
                if (Visited.Peek() != Position + Facing)
                {
                    Facing = Visited.Peek() - Position;
                    return;
                }
                
                if (IsBacktracking)
                    BadPositions.Add(Position);
                
                Position = IsResetting ? Visited.Peek() : Visited.Pop();
                
                if (Position == DecisionPoints.Peek())
                {
                    DecisionPoints.Pop();
                    IsUnwinding = false;
                    IsBacktracking = false;
                    IsResetting = false;
                }

                return;
            }

            if (IsBlocked)
            {
                // 0,1 -> 1,0 -> 0,-1 -> -1, 0
                Facing = Vector2D.Zero - Facing;
                IsBacktracking = true;
                return;
            }
            
            var available = EnumeratePossibleNextSteps().ToHashSet();
            
            if (DecisionPoints.Contains(Position + Facing) || available.Count == 0)
            {
                // 0,1 -> 1,0 -> 0,-1 -> -1, 0
                Facing = Vector2D.Zero - Facing;
                IsUnwinding = true;
                return;
            }

            // 1st preference - not visited
            if (available.Contains(Position + Facing))
            {
                if (available.Count > 1)
                {
                    DecisionPoints.Push(Position);
                }
                
                Visited.Push(Position);
                Position += Facing;
            }
            else
            {
                var next = available.First();
                Facing = next - Position;
            }
        }

        public MazeRunner CreateSecondaryRunner()
        {
            var visited = new Stack<Vector2D>(Visited.Reverse());
            
            var lastDecisionPoint = DecisionPoints.Pop();
            var index = visited.Count - 1;
            
            while(visited.ElementAt(index) != lastDecisionPoint)
            {
                index--;
            }
            
            return new MazeRunner(Maze, lastDecisionPoint, Target)
            {
                BadPositions = BadPositions,
                DecisionPoints = DecisionPoints,
                Facing = visited.ElementAt(index) - visited.ElementAt(index - 1),
                Visited = visited
            };
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