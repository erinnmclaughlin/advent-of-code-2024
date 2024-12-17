﻿using System.Text;
using AoC.CSharp.Common;
using Xunit.Abstractions;

namespace AoC;

public class Day16(ITestOutputHelper output)
{
    //private readonly string[] _fileLines = File.ReadAllLines("day16.txt");

    [Theory]
    [InlineData("day16.example1.txt", 7036)]
    [InlineData("day16.example2.txt", 11048)]
    [InlineData("day16.txt", 147628)]
    public void PartOne(string path, int expected)
    {
        var maze = CSharp.Day16.MazeModel.Parse(File.ReadAllLines(path));
        Solve(maze);
        
        foreach (var runner in maze.Runners)
        {
            output.WriteLine("Runner {0}: Score: {1}; Tiles: {2}", runner.Id, runner.Score, runner.Visited.Count);
        }
        
        Assert.Equal(expected, maze.Runners.Min(x => x.Score));
    }
    
    // 628 is too low
    
    [Theory]
    [InlineData("day16.example1.txt", 45)]
    [InlineData("day16.example2.txt", 64)]
    [InlineData("day16.txt", 0)]
    public void PartTwo(string path, int expected)
    {
        var maze = CSharp.Day16.MazeModel.Parse(File.ReadAllLines(path));
        Solve(maze);

        var bestScore = maze.Runners.Min(x => x.Score);

        var bestSeats = maze.Runners
            .Where(x => x.Score == bestScore)
            .SelectMany(x => x.Visited.Keys)
            .ToHashSet();

        for (var y = 0; y < maze.Height; y++)
        {
            var sb = new StringBuilder();
            
            for (var x = 0; x < maze.Width; x++)
            {
                var pos = new Vector2D(x, y);

                if (maze.Walls.ContainsKey(pos))
                    sb.Append('#');
                else if (bestSeats.Contains(pos))
                    sb.Append('O');
                else
                    sb.Append('.');
            }
            
            output.WriteLine(sb.ToString());
        }
        
        Assert.Equal(expected, bestSeats.Count);
    }

    private static void Solve(CSharp.Day16.MazeModel maze)
    {
        while (maze.Runners.Any(x => x.Position != maze.Target))
        {
            maze.Runners.RemoveWhere(x => x.IsDead);
            var runners = maze.Runners.Where(x => !x.IsDead).ToList();

            foreach (var runner in runners)
            {
                if (runner.Move()) continue;
            
                foreach (var clone in runner.Clones)
                    maze.Runners.Add(clone);

                if (runner.IsDead)
                    maze.Runners.Remove(runner);
            }
        }
    }
}