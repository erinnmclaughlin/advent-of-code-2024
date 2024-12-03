using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace AdventOfCode2024.Day03;

public sealed class Day03(ITestOutputHelper output)
{
    [Fact]
    public void PartOne()
    {
        var sum = 0;
        var input = ProcessFile();
        
        foreach (Match match in Regex.Matches(input, @"mul\(\d+,\d+\)"))
        {
            var numbers = Regex.Matches(match.Value, @"\d+");
            sum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
        }
        
        output.WriteLine(sum.ToString());
        Assert.Equal(161085926, sum);
    }

    [Fact]
    public void PartTwo()
    {
        var sum = 0;
        var input = ProcessFile();

        var dos = Regex.Matches(input, @"do\(\)").Select(m => m.Index).ToArray();
        var donts = Regex.Matches(input, @"don't\(\)").Select(m => m.Index).ToArray();
        
        foreach (Match match in Regex.Matches(input, @"mul\(\d+,\d+\)"))
        {
            var closestDo = dos.LastOrDefault(x => x < match.Index);
            var closestDont = donts.LastOrDefault(x => x < match.Index);

            if (closestDont > closestDo)
                continue;
            
            var numbers = Regex.Matches(match.Value, @"\d+");
            sum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
        }
        
        output.WriteLine(sum.ToString());
        Assert.Equal(82045421, sum);
    }

    private static string ProcessFile()
    {
        return File.ReadAllText(Path.Combine("Day03", "input.txt"));
    }
}