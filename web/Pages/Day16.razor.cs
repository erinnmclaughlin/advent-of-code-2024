﻿using System.Collections.Immutable;
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

        foreach (var group in Maze.Runners.GroupBy(x => (x.Position, x.Facing)))
        foreach (var runner in group.OrderBy(x => x.Score).Skip(1))
            runner.IsDead = true;
        
        Maze.Runners.RemoveWhere(x => x.IsDead);
        
        var runners = Maze.Runners.ToList();

        foreach (var runner in runners)
            runner.Move();
        
        Maze.Runners.RemoveWhere(x => x.IsDead);
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

        _ = maze.SpawnRunner(start);

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
        public HashSet<Vector2D> Visited { get; set; } = [];

        public IEnumerable<Vector2D> EnumeratePossibleNextSteps() => _maze
            .EnumerateAdjacentPaths(Position)
            .Where(position => !_maze.Visited.Contains((position, Position - position)));

        public void Move()
        {
            if (IsDead || Position == _maze.Target)
                return;
            
            Visited.Add(Position);

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
                clone.Visited = Visited.ToHashSet();
            }
            
            if (possibilities.Contains(Position + Facing))
            {
                Score++;
                Position += Facing;
            }
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