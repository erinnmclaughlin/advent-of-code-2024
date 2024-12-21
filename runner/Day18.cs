using System.Collections.Immutable;
using System.Text;
using AoC.CSharp.Common;
using Xunit.Abstractions;

namespace AoC;

public class Day18(ITestOutputHelper output)
{
    [Theory(Skip = "Skipped Day 18")]
    [InlineData("day18.example.txt", 7, 22)]
    public void PartOne(string inputFile, int size, int expected)
    {
        var input = new StringBuilder[size];

        for (var i = 0; i < size; i++)
            input[i] = new StringBuilder(string.Join("", Enumerable.Repeat('.', size)));

        foreach (var line in File.ReadAllLines(inputFile))
        {
            var split = line.Split(',');
            var (x, y) = (int.Parse(split[1]), int.Parse(split[0]));
            input[y][x] = '#';
        }
        
        foreach (var sb in input)
            output.WriteLine(sb.ToString());
        
        var target = new Vector2D(size, size);
        var maze = Maze2D.Parse(input.Select(x => x.ToString()).ToArray());
        output.WriteLine("");
        output.WriteLine(maze.GetString());
    }
}