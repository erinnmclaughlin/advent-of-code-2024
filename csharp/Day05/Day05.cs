namespace AdventOfCode2024.Day05;

public class Day05
{
    private readonly CustomComparer _comparer;
    private readonly List<int[]> _numbers;

    public Day05()
    {
        var lines = File.ReadLines(Path.Combine("Day05", "input.txt")).ToArray().AsSpan();
        var splitIndex = lines.IndexOf("");
        
        _comparer = new CustomComparer(lines[..splitIndex].ToArray());
        _numbers = lines[(splitIndex + 1)..]
            .ToArray()
            .Select(x => x.Split(',').Select(int.Parse).ToArray())
            .ToList();
    }

    [Fact]
    public void PartOne()
    {
        var sum = _numbers
            .Where(IsOrdered)
            .Sum(x => x[x.Length / 2]);
        
        Assert.Equal(5268, sum);
    }
    
    [Fact]
    public void PartTwo()
    {
        var sum = _numbers
            .Where(x => !IsOrdered(x))
            .Select(x => x.Order(_comparer).ToArray())
            .Sum(x => x[x.Length / 2]);

        Assert.Equal(5799, sum);
    }

    private bool IsOrdered(int[] items) => items.SequenceEqual(items.Order(_comparer));
    
    private class CustomComparer(string[] lines) : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var span = lines.AsSpan();
            return span.Contains($"{x}|{y}") ? -1 : 1;
        }
    }
}
