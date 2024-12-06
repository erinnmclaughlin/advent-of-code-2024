using System.Text.RegularExpressions;

namespace AoC.CSharp;

public static partial class Day03
{
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();
    
    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    private static partial Regex MulDoDontRegex();
    
    public static int PartOne(string fileText) => MulRegex()
        .Matches(fileText)
        .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
    
    public static int PartTwo(string fileText) => MulDoDontRegex()
        .Matches(fileText)
        .Aggregate((IsMatchEnabled: true, Sum: 0), (x, match) =>
        {
            if (match.Value.StartsWith("mul") && x.IsMatchEnabled)
                return (true, x.Sum + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

            return (match.Value == "do()", x.Sum);
        })
        .Sum;
}
