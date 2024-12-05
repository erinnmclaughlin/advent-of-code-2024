using Xunit.Abstractions;

namespace AdventOfCode2024.Day05;

public class Day05(ITestOutputHelper? output)
{
    private readonly string[] _fileLines = File.ReadLines(Path.Combine("Day05", "input.txt")).ToArray();

    [Fact]
    public void PartOne()
    {
        var (rules, updates) = ParseFile();

        var sum = updates
            .Where(u => rules.All(r => r.Invoke(u)))
            .Sum(u => u[u.Count / 2]);

        output?.WriteLine("{0}", sum);
        Assert.Equal(5268, sum);
    }
    
    [Fact]
    public void PartTwo()
    {
        var (rules, updates) = ParseFile();

        var sum = updates
            .Select(u => new { Original = u, Updated = DoOrdering(u, rules) })
            .Where(x => !x.Original.SequenceEqual(x.Updated))
            .Sum(x => x.Updated[x.Updated.Count / 2]);
        
        output?.WriteLine("{0}", sum);
        Assert.Equal(5799, sum);
    }

    private (List<Func<List<int>, bool>> Rules, List<List<int>> Updates) ParseFile()
    {
        var buildingRules = true;
        var rules = new List<Func<List<int>, bool>>();
        var updates = new List<List<int>>();
        
        foreach (var line in _fileLines)
        {
            if (string.IsNullOrWhiteSpace(line))
                buildingRules = false;
            else if (buildingRules)
                rules.Add(GetRule(line));
            else
                updates.Add(line.Split(',').Select(int.Parse).ToList());
        }

        return (rules, updates);
    }

    private static Func<List<int>, bool> GetRule(string line)
    {
        var numbers = line.Split('|');
        var n1 = int.Parse(numbers[0]);
        var n2 = int.Parse(numbers[1]);

        return arr =>
        {
            if (!arr.Contains(n1) || !arr.Contains(n2))
                return true;
                
            return arr.IndexOf(n1) <= arr.IndexOf(n2);
        };
    }
    
    private List<int> DoOrdering(List<int> list, List<Func<List<int>, bool>> rules)
    {
        var arr = list.ToList();

        while (!rules.All(r => r.Invoke(arr)))
        {
            foreach (var line in _fileLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    break;

                // todo: refactor this out to share with GetRule method
                
                var numbers = line.Split('|');
                var n1 = int.Parse(numbers[0]);
                var n2 = int.Parse(numbers[1]);

                if (!arr.Contains(n1) || !arr.Contains(n2))
                    continue;

                var i1 = arr.IndexOf(n1);
                var i2 = arr.IndexOf(n2);

                while (i1 > i2)
                {
                    arr[i1] = arr[i1 - 1];
                    i1--;
                    arr[i1] = n1;
                }
            }
        }

        return arr;
    }
}
