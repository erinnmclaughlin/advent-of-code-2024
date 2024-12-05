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
        var sum = 0;

        foreach (var update in GetUpdates())
        {
            var sorted = DoOrdering(update);
            
            if (update.SequenceEqual(sorted))
                continue;
            
            //output.WriteLine("{0}\n{1}\n\n", string.Join(',', update), string.Join(',', sorted));
            
            sum += sorted[sorted.Count / 2];
        }
        
        output?.WriteLine("{0}", sum);
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
                
                return arr.IndexOf(n1) <= arr.IndexOf(n2);
            };
        }
    }

    private List<int> DoOrdering(List<int> list)
    {
        var arr = list.ToList();
        var rules = GetRules().ToList();

        while (!rules.All(r => r.Invoke(arr)))
        {
            foreach (var line in _fileLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    break;

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
