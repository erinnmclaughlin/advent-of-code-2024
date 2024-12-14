using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace AoC;

public class Day14(ITestOutputHelper output)
{
    private readonly string[] _input = File.ReadAllLines("day14.txt");
    
    [Fact]
    public void PartOne()
    {
        var robots = _input.Select(Robot.Parse).ToList();
        
        const int numCols = 101;
        const int numRows = 103;
        
        robots.ForEach(x => x.Move(100, numRows, numCols));

        var q1 = robots.Count(x => x.Position is { X: < numCols / 2, Y: < numRows / 2 });
        var q2 = robots.Count(x => x.Position is { X: < numCols / 2, Y: > numRows / 2 });
        var q3 = robots.Count(x => x.Position is { X: > numCols / 2, Y: > numRows / 2 });
        var q4 = robots.Count(x => x.Position is { X: > numCols / 2, Y: < numRows / 2 });

        var score = q1 * q2 * q3 * q4;
        output.WriteLine(score.ToString());
    }
    
    private sealed class Robot
    {
        public (int X, int Y) Position { get; set; }
        public (int dX, int dY) Velocity { get; set; }

        public void Move(int seconds, int numRows, int numCols)
        {
            var x = (Position.X + Velocity.dX * seconds) % numCols;
            var y = (Position.Y + Velocity.dY * seconds) % numRows;

            if (x < 0) x = numCols + x;
            if (y < 0) y = numRows + y;
            
            Position = (x, y);
        }
        
        public static Robot Parse(string input)
        {
            var splitIndex = input.IndexOf(' ');
            var pos = input[2..splitIndex].Split(',');
            var vel = input[(splitIndex + 3)..].Split(',');
            return new Robot
            {
                Position = (int.Parse(pos[0]), int.Parse(pos[1])),
                Velocity = (int.Parse(vel[0]), int.Parse(vel[1]))
            };
        }
    }
}