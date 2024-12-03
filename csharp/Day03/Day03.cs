using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;

public sealed class Day03
{
    [Fact]
    public void PartOne()
    {
        var sum = Regex
            .Matches(ReadFile(), @"mul\((\d+),(\d+)\)")
            .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
        
        Assert.Equal(161085926, sum);
    }

    [Fact]
    public void PartTwo()
    {
        var (_, sum) = Regex
            .Matches(ReadFile(), @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")
            .Aggregate((IsMatchEnabled: true, Sum: 0), (x, match) =>
            {
                if (match.Value.StartsWith("mul") && x.IsMatchEnabled)
                    return (true, x.Sum + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
                
                return (match.Value == "do()", x.Sum);
            });
        
        Assert.Equal(82045421, sum);
    }
    
    private static string ReadFile() => File.ReadAllText(Path.Combine("Day03", "input.txt"));
}