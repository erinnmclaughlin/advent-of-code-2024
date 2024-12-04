using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day04;

public partial class Day04
{
    [Fact, Benchmark]
    public void Part01_Optimized()
    {
        short count = 0;
        
        var lines = _fileLines.AsSpan();
        
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].AsSpan();
            
            for (var j = 0; j < lines[0].Length; j++)
            {
                if (line[j] != 'X')
                    continue;
                
                Direction direction = 0;
                if (i - 3 >= 0) direction |= Direction.Up;
                if (i + 3 < lines.Length) direction |= Direction.Down;
                if (j - 3 >= 0) direction |= Direction.Left;
                if (j + 3 < lines[0].Length) direction |= Direction.Right;

                // up
                if (direction.HasFlag(Direction.Up) && lines[i - 1][j] == 'M' && lines[i - 2][j] == 'A' && lines[i - 3][j] == 'S')
                    count++;
                
                // down
                if (direction.HasFlag(Direction.Down) && lines[i + 1][j] == 'M' && lines[i + 2][j] == 'A' && lines[i + 3][j] == 'S')
                    count++;

                // left
                if (direction.HasFlag(Direction.Left) && line[j - 1] == 'M' && line[j - 2] == 'A' && line[j - 3] == 'S')
                    count++;
                
                // right
                if (direction.HasFlag(Direction.Right) && line[j + 1] == 'M' && line[j + 2] == 'A' && line[j + 3] == 'S')
                    count++;
                
                // up + left
                if (direction.HasFlag(Direction.UpLeft) && lines[i - 1][j - 1] == 'M' && lines[i - 2][j - 2] == 'A' && lines[i - 3][j - 3] == 'S')
                    count++;
                
                // up + right
                if (direction.HasFlag(Direction.UpRight) && lines[i - 1][j + 1] == 'M' && lines[i - 2][j + 2] == 'A' && lines[i - 3][j + 3] == 'S')
                    count++;
                
                // bottom + left
                if (direction.HasFlag(Direction.DownLeft) && lines[i + 1][j - 1] == 'M' && lines[i + 2][j - 2] == 'A' && lines[i + 3][j - 3] == 'S')
                    count++;
                
                // bottom + right
                if (direction.HasFlag(Direction.DownRight) && lines[i + 1][j + 1] == 'M' && lines[i + 2][j + 2] == 'A' && lines[i + 3][j + 3] == 'S')
                    count++;
            }
        }

        Assert.Equal(2500, count);
    }

    [Fact, Benchmark]
    public void Part02_Optimized()
    {
        short count = 0;

        var lines = _fileLines.AsSpan();
        
        // pad cols & rows by 1 to ensure there's room for the "X" shape
        for (var i = 1; i < lines.Length - 1; i++)
        {
            for (var j = 1; j < lines[0].Length - 1; j++)
            {
                if (lines[i][j] is not 'A')
                    continue;

                var topLeft = lines[i - 1][j - 1];
                if (topLeft is not 'M' and not 'S') continue;

                var bottomRight = lines[i + 1][j + 1];
                if (bottomRight is not 'M' and not 'S') continue;
                
                if (topLeft == bottomRight) continue;

                var topRight = lines[i - 1][j + 1];
                if (topRight is not 'M' and not 'S') continue;

                var bottomLeft = lines[i + 1][j - 1];
                if (bottomLeft is not 'M' and not 'S') continue;
                
                if (topRight == bottomLeft) continue;
                
                count++;
            }
        }
        
        Assert.Equal(1933, count);
    }

    [Flags]
    private enum Direction : byte
    {
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        UpLeft = Up | Left,
        UpRight = Up | Right,
        DownLeft = Down | Left,
        DownRight = Down | Right,
    }
}