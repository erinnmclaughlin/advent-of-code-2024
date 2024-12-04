using Xunit.Abstractions;

namespace AdventOfCode2024.Day04;

public sealed class Day04(ITestOutputHelper? output = null)
{
    private readonly string[] _fileLines = File.ReadLines(Path.Combine("Day04", "input.txt")).ToArray();
    
    [Fact]
    public void Part01()
    {
        var horizontal = CountHorizontal(_fileLines);
        var vertical = CountVertical(_fileLines);
        var diagonal = CountDiagonal(_fileLines) + CountDiagonal(_fileLines.Reverse().ToArray());

        var sum = horizontal + vertical + diagonal;
        
        output?.WriteLine(sum.ToString());
        Assert.Equal(2500, sum);
    }

    [Fact]
    public void Part02()
    {
        var chars = File.ReadAllLines("Day04/input.txt").Select(l => l.ToCharArray()).ToArray();
        var count = 0;
        
        for (var i = 0; i < chars.Length; i++)
        {
            for (var j = 0; j < chars[i].Length; j++)
            {
                if (chars[i][j] is not 'A')
                    continue;

                try
                {
                    var topLeft = chars[i - 1][j - 1];
                    var bottomRight = chars[i + 1][j + 1];
                
                    if (topLeft is not 'M' and not 'S' ||
                        bottomRight is not 'M' and not 'S')
                        continue;
                
                    if (topLeft is 'S' && bottomRight is not 'M')
                        continue;
                
                    if (topLeft is 'M' && bottomRight is not 'S')
                        continue;

                    var topRight = chars[i + 1][j - 1];
                    var bottomLeft = chars[i - 1][j + 1];

                    if (topRight is not 'M' and not 'S' ||
                        bottomLeft is not 'M' and not 'S')
                        continue;

                    if (topRight is 'S' && bottomLeft is not 'M')
                        continue;
                
                    if (topRight is 'M' && bottomLeft is not 'S')
                        continue;
                    
                    count++;
                }
                catch
                {
                    
                }
            }
        }
        
        output?.WriteLine(count.ToString());
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
            var next = line[i..];
            if (next.StartsWith("XMAS") || next.StartsWith("SAMX"))
                count++;
        }
        
        return count;
    }
}