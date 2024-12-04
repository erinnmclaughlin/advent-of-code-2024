using BenchmarkDotNet.Attributes;
using Xunit.Abstractions;

namespace AdventOfCode2024.Day04;

[MemoryDiagnoser]
public class Day04(ITestOutputHelper? output = null)
{
    private readonly string[] _fileLines = File.ReadLines(Path.Combine("Day04", "input.txt")).ToArray();
    
    [Fact, Benchmark]
    public void Part01()
    {
        var horizontal = CountHorizontal(_fileLines);
        var vertical = CountVertical(_fileLines);
        var diagonal = CountDiagonal(_fileLines) + CountDiagonal(_fileLines.Reverse().ToArray());

        var sum = horizontal + vertical + diagonal;
        
        Assert.Equal(2500, sum);
    }

    [Fact, Benchmark]
    public void Part02()
    {
        var count = 0;
        
        // pad cols & rows by 1 to ensure there's room for the "X" shape
        for (var i = 1; i < _fileLines.Length - 1; i++)
        {
            for (var j = 1; j < _fileLines[i].Length - 1; j++)
            {
                if (_fileLines[i][j] is not 'A')
                    continue;

                var letters = new
                {
                    TopLeft = _fileLines[i - 1][j - 1],
                    TopRight = _fileLines[i - 1][j + 1],
                    BottomLeft = _fileLines[i + 1][j - 1],
                    BottomRight = _fileLines[i + 1][j + 1]
                };
                
                // make sure top left to bottom right diagonal is "MAS" or "SAM"
                if (letters is not { TopLeft: 'M', BottomRight: 'S' } and not { TopLeft: 'S', BottomRight: 'M' })
                    continue;
                
                // make sure top right to bottom left diagonal is "MAS" or "SAM"
                if (letters is not { TopRight: 'M', BottomLeft: 'S' } and not { TopRight: 'S', BottomLeft: 'M' })
                    continue;
                
                count++;
            }
        }
        
        Assert.Equal(1933, count);
    }
    
    private static int CountHorizontal(string[] lines)
    {
        return lines.Sum(line => CountXmas(line));
    }

    private static int CountVertical(string[] lines)
    {
        var sum = 0;
        
        for (var i = 0; i < lines[0].Length; i++)
        {
            var newLine = new char[lines.Length];
            
            for (var j = 0; j < lines.Length; j++)
            {
                newLine[j] = lines[j][i];
            }

            sum += CountXmas(newLine);
        }

        return sum;
    }

    private static int CountDiagonal(string[] lines)
    {
        var sum = 0;

        for (var i = 0; i < lines.Length - 3; i++)
        {
            for (var j = 0; j < lines[i].Length - 3; j++)
            {
                // just need to grab 4 chars at a time; we'll loop around to the rest of the diagonal
                char[] chars =
                [
                    lines[i][j],
                    lines[i + 1][j + 1],
                    lines[i + 2][j + 2],
                    lines[i + 3][j + 3]
                ];
                
                sum += CountXmas(chars);
            }
        }
        
        return sum;
    }

    private static int CountXmas(ReadOnlySpan<char> line)
    {
        var count = 0;

        for (var i = 0; i < line.Length; i++)
        {
            if (line[i..].StartsWith("XMAS") || line[i..].StartsWith("SAMX"))
                count++;
        }
        
        return count;
    }
    
    [BenchmarkRunner]
    public void Benchmarks()
    {
        var summary = BenchmarkRunner.Run<Day04>();
        output?.WriteLine(summary);
    }
}