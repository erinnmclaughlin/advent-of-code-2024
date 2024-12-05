using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;

public static class Solution
{
    public static int PartOne(string fileText) => Regex
        .Matches(fileText, @"mul\((\d+),(\d+)\)")
        .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
    
    public static int PartTwo(string fileText) => Regex
        .Matches(fileText, @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")
        .Aggregate((IsMatchEnabled: true, Sum: 0), (x, match) =>
        {
            if (match.Value.StartsWith("mul") && x.IsMatchEnabled)
                return (true, x.Sum + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

            return (match.Value == "do()", x.Sum);
        })
        .Sum;
}
