using System.Collections;
using AoC.CSharp.Common;
using Microsoft.AspNetCore.Components;

namespace AoC.Web.Pages;

public partial class Day16 : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;
    
    private Maze2D? Maze { get; set; }
    private Vector2D Current { get; set; }
    private Direction Direction { get; set; } = Direction.Right;
    private Vector2D Start { get; set; }
    private Vector2D Target { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var text = await HttpClient.GetStringAsync("day16.example.txt");
        var lines = text.Replace("\r", "").Split('\n');

        var maze = new Maze2D(lines.Length, lines[0].Length);
        
        for (var y = 0; y < maze.Height; y++)
        for (var x = 0; x < maze.Width; x++)
        {
            var character = lines[y][x];

            if (character == '.')
                continue;

            var position = new Vector2D(x, y);

            switch (character)
            {
                case '#':
                    maze.Walls.Add(position);
                    break;
                case 'S':
                    Start = position;
                    Current = position;
                    break;
                case 'E':
                    Target = position;
                    break;
            }
        }

        Maze = maze;
    }

    private long CalculateCost(Vector2D nextPosition)
    {
        long cost = 0;
        var dir = Direction;
        var position = Current;

        while (position != nextPosition)
        {
            var nextDir = Maze!
                .EnumerateOpenDirections(position)
                .Single(nextDir => nextDir != dir.GetOpposite());

            cost += dir == nextDir ? 1001 : 1;
            
            dir = nextDir;
            position = nextPosition;
        }

        return cost;
    }

    private long CalculateUnitCost(Direction direction)
    {
        return CalculateUnitCost(Direction, direction);
    }

    private static long CalculateUnitCost(Direction facing, Direction nextFacing)
    {
        return facing == nextFacing ? 1 : 1001;
    }

    private Dictionary<Vector2D, long> GetScoresAtPoints(Vector2D position, Direction direction)
    {
        var dict = new Dictionary<Vector2D, long> { { position, 0 } };

        var points = GetNextDecisionPoints(position, direction);
        
        foreach (var (next, _, cost) in points)
        {
            dict.Add(next, cost);
        }

        foreach (var (next, dir, cost) in points)
        {
            CalculateScoresAtPoints(next, dir, cost, dict);
        }

        return dict;
    }

    private void CalculateScoresAtPoints(Vector2D position, Direction direction, long cost, Dictionary<Vector2D, long> dict)
    {
        foreach (var (next, dir, c) in GetNextDecisionPoints(position, direction).OrderBy(x => x.Cost))
        {
            var newCost = cost + c;

            if (dict.TryAdd(next, newCost) || dict[next] > newCost)
            {
                dict[next] = newCost;
                
                if (next != Target)
                    CalculateScoresAtPoints(next, dir, newCost, dict);
            }
        }
    }

    private List<(Vector2D NextPosition, Direction NextDirection, long Cost)> GetNextDecisionPoints()
    {
        return GetNextDecisionPoints(Current, Direction);
    }
    
    private List<(Vector2D NextPosition, Direction NextDirection, long Cost)> GetNextDecisionPoints(Vector2D position, Direction direction)
    {
        var next = EnumerateNextDecisionPoints(position, direction).ToList();

        while (next.Count == 1)
        {
            next = EnumerateNextDecisionPoints(next[0].NextPosition, next[0].NextDirection, next[0].Cost).ToList();
        }

        return next;
    }
    
    private IEnumerable<(Vector2D NextPosition, Direction NextDirection, long Cost)> EnumerateNextDecisionPoints(Vector2D currentPosition, Direction currentDirection, long currentCost = 0)
    {
        foreach (var nextDir in Maze!.EnumerateOpenDirections(currentPosition).Where(x => x != currentDirection.GetOpposite()))
        {
            var pos = currentPosition + nextDir;
            var cost = currentCost + CalculateUnitCost(currentDirection, nextDir);
            var dir = nextDir;
            
            while (pos != Target)
            {
                var nextDirs = Maze.EnumerateOpenDirections(pos).Where(x => x != dir.GetOpposite()).ToList();

                if (nextDirs.Count == 0)
                    yield break;
                
                if (nextDirs.Count != 1)
                    break;
                
                pos += nextDirs[0];
                cost += CalculateUnitCost(dir, nextDirs[0]);
                dir = nextDirs[0];
            }

            yield return (pos, dir, cost);
        }
    }
}