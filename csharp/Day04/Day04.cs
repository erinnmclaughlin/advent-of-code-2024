using Xunit.Abstractions;

namespace AdventOfCode2024.Day04;

public sealed class Day04(ITestOutputHelper? output = null)
{
    [Fact]
    public void Part01()
    {
        var chars = File.ReadAllLines("Day04\\input.txt").Select(l => l.ToCharArray()).ToArray();
        
        var horizontal = CountHorizontal(chars);
        var horizontalReverse = CountHorizontal(chars.Select(x => x.Reverse().ToArray()).ToArray());
        
        var vertical = CountVertical(chars);
        var verticalReverse = CountVertical(chars.Reverse().ToArray());

        var diagonal1 = CountDiagonal(chars);
        var diagonal2 = CountDiagonal(chars.Reverse().Select(c => c.Reverse().ToArray()).ToArray());
        var diagonal3 = CountDiagonal(chars.Reverse().ToArray());
        var diagonal4 = CountDiagonal(chars.Select(x => x.Reverse().ToArray()).ToArray());
        
        var sum =  horizontal + 
                   horizontalReverse + 
                   vertical + 
                   verticalReverse +
                   diagonal1 + 
                   diagonal2 +
                   diagonal3 +
                   diagonal4
                   ;
        
        output?.WriteLine(sum.ToString());
    }

    [Fact]
    public void Part02()
    {
        var chars = File.ReadAllLines("Day04\\input.txt").Select(l => l.ToCharArray()).ToArray();
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
    
    private static int CountHorizontal(char[][] lines)
    {
        var sum = 0;
        foreach (var line in lines)
        {
            var count = CountXmas(line);
            sum += count;
        }

        return sum;
    }

    private int CountDiagonal(char[][] lines)
    {
        var sum = 0;
        var max = Math.Max(lines.Length, lines[0].Length);

        for (var i = 0; i < max; i++)
        {
            for (var j = 0; j < max; j++)
            {
                try
                {
                    var newLine = new char[4];
                    newLine[0] = lines[i][j];
                    newLine[1] = lines[i + 1][j + 1];
                    newLine[2] = lines[i + 2][j + 2];
                    newLine[3] = lines[i + 3][j + 3];

                    if (string.Join("", newLine) == "XMAS")
                        sum++;
                }
                catch
                {
                }
            }
        }

        return sum;
    }
    
    private static int CountVertical(char[][] lines)
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

    private static int CountXmas(char[] line)
    {
        var count = 0;
        var nextChar = 'X';
        
        foreach (var c in line)
        {
            if (c != nextChar)
            {
                if (c == 'X')
                {
                    nextChar = 'M';
                    continue;
                }
                
                nextChar = 'X';
                continue;
            }

            nextChar = c switch
            {
                'X' => 'M',
                'M' => 'A',
                'A' => 'S',
                'S' => 'X'
            };

            if (nextChar == 'X')
                count++;
        }

        return count;
    }
}