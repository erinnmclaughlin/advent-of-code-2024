using Xunit.Abstractions;

namespace AdventOfCode2024.Day05;

public class Day05(ITestOutputHelper? output)
{
    private readonly string[] _fileLines = File.ReadLines(Path.Combine("Day05", "input.txt")).ToArray();

    [Fact]
    public void PartOne()
    {
        var rules = GetRules().ToList();
        
        var sum = 0;

        foreach (var update in GetUpdates())
        {
            if (rules.All(r => r.Invoke(update)))
                sum += update[update.Count / 2];
        }
        
        output?.WriteLine("{0}", sum);
    }
    
    [Fact]
    public void PartTwo()
    {
        foreach (var line in _fileLines)
            output?.WriteLine(line);
    }

    private IEnumerable<List<int>> GetUpdates()
    {
        var skip = true;
        foreach (var line in _fileLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                skip = false;
                continue;
            }
            
            if (skip) continue;

            yield return line.Split(',').Select(int.Parse).ToList();
        }
    }
    
    private IEnumerable<Func<List<int>, bool>> GetRules()
    {
        foreach (var line in _fileLines)
        {
            if (string.IsNullOrWhiteSpace(line))
                yield break;

            var numbers = line.Split('|');
            var n1 = int.Parse(numbers[0]);
            var n2 = int.Parse(numbers[1]);

            yield return arr =>
            {
                if (!arr.Contains(n1) || !arr.Contains(n2))
                    return true;
                
                return arr.IndexOf(n1) < arr.IndexOf(n2);
            };
        }
    }
}