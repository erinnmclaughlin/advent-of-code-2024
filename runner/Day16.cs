using AoC.CSharp.Common;

namespace AoC;

public class MazePositionState
{
    public MazePositionState? PreviousPosition { get; set; }
    
    public Direction Direction { get; set; }
    public Vector2D Position { get; set; }
    public int Cost { get; set; }

    public MazeSolution ToSolution()
    {
        var solution = new MazeSolution { Cost = Cost };

        solution.Paths.Add((Direction, Position), Cost);

        var previous = PreviousPosition;
        while (previous != null)
        {
            solution.Paths.Add((previous.Direction, previous.Position), previous.Cost);
            previous = previous.PreviousPosition;
        }

        return solution;
    }
}

public class MazeSolution
{
    public int Cost { get; set; }
    public Dictionary<(Direction Direction, Vector2D Position), int> Paths { get; } = [];
}

public class Day16
{
    [Theory]
    [InlineData("day16.example1.txt", 7036)]
    [InlineData("day16.example2.txt", 11048)]
    [InlineData("day16.txt", 147628)]
    public void PartOne(string filePath, int expected)
    {
        var (maze, start, target) = CreateStateContainer(File.ReadAllLines(filePath));
        var solution = FindBestSolution(maze, start, target);
        Assert.Equal(expected, solution?.Cost);
    }
    
    [Theory]
    [InlineData("day16.example1.txt", 45)]
    [InlineData("day16.example2.txt", 64)]
    [InlineData("day16.txt", 670)]
    public void PartTwo(string filePath, int expected)
    {
        var (maze, start, target) = CreateStateContainer(File.ReadAllLines(filePath));

        var solutions = EnumerateBestSolutions(maze, start, target).SelectMany(x => x.Paths.Keys)
            .Select(x => x.Position).Distinct().Count();
        
        Assert.Equal(expected, solutions);
    }
    
    private static MazeSolution? FindBestSolution(Maze2D maze, Vector2D start, Vector2D target)
    {
        var seen = new HashSet<(Direction, Vector2D)>();
        var queue = new PriorityQueue<MazePositionState, int>();

        var initialState = new MazePositionState
        {
            PreviousPosition = null,
            Direction = Direction.Right,
            Position = start,
            Cost = 0
        };
        
        queue.Enqueue(initialState, 0);

        while (queue.TryDequeue(out var current, out var cost))
        {
            if (!seen.Add((current.Direction, current.Position)))
                continue;
            
            if (current.Position == target)
            {
                return current.ToSolution();
            }

            var currentDirection = current.Direction;
            var oppositeDirection = current.Direction.GetOpposite();
            
            foreach (var item in maze.EnumerateOpenAdjacentPaths(current.Position))
            {
                if (item.Direction == oppositeDirection) continue;

                var nextState = new MazePositionState
                {
                    PreviousPosition = current,
                    Direction = item.Direction,
                    Position = item.Position,
                    Cost = cost + (item.Direction == currentDirection ? 1 : 1001)
                };
                
                queue.Enqueue(nextState, nextState.Cost);
            }
        }

        return null;
    }
    
    private static IEnumerable<MazeSolution> EnumerateBestSolutions(Maze2D maze, Vector2D start, Vector2D target)
    {
        var solution = FindBestSolution(maze, start, target);
        if (solution is null) yield break;
        
        var seen = new HashSet<(Direction, Vector2D)>();
        var queue = new PriorityQueue<MazePositionState, int>();

        var initialState = new MazePositionState
        {
            PreviousPosition = null,
            Direction = Direction.Right,
            Position = start,
            Cost = 0
        };
        
        queue.Enqueue(initialState, 0);

        while (queue.TryDequeue(out var current, out var cost))
        {
            var state = (current.Direction, current.Position);
            
            if (!solution.Paths.ContainsKey(state) && !seen.Add((current.Direction, current.Position)))
                continue;
            
            if (current.Position == target)
            {
                if (current.Cost == solution.Cost)
                {
                    yield return current.ToSolution();
                }
                
                continue;
            }

            var currentDirection = current.Direction;
            var oppositeDirection = current.Direction.GetOpposite();
            
            foreach (var item in maze.EnumerateOpenAdjacentPaths(current.Position))
            {
                if (item.Direction == oppositeDirection) continue;

                var nextState = new MazePositionState
                {
                    PreviousPosition = current,
                    Direction = item.Direction,
                    Position = item.Position,
                    Cost = cost + (item.Direction == currentDirection ? 1 : 1001)
                };
                
                queue.Enqueue(nextState, nextState.Cost);
            }
        }
    }
    
    private static (Maze2D maze, Vector2D start, Vector2D target) CreateStateContainer(string[] fileLines)
    {
        var maze = new Maze2D(fileLines.Length, fileLines[0].Length);
        var start = Vector2D.Zero;
        var target = Vector2D.Zero;
        
        for (var y = 0; y < maze.Height; y++)
        for (var x = 0; x < maze.Width; x++)
        {
            var character = fileLines[y][x];

            if (character == '.')
                continue;

            var position = new Vector2D(x, y);

            switch (character)
            {
                case '#':
                    maze.Walls.Add(position);
                    break;
                case 'S':
                    start = position;
                    break;
                case 'E':
                    target = position;
                    break;
            }
        }

        return (maze, start, target);
    }
}