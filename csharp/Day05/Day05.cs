namespace AdventOfCode2024.Day05;

public class Day05
{
    private readonly IComparer<string> _comparer;
    private readonly string[][] _numbers;

    public Day05()
    {
        var lines = File.ReadLines(Path.Combine("Day05", "input.txt")).ToArray().AsSpan();
        var splitIndex = lines.IndexOf("");
        
        _comparer = CreateComparer(lines[..splitIndex].ToArray());
        _numbers = lines[(splitIndex + 1)..]
            .ToArray()
            .Select(x => x.Split(',').ToArray())
            .ToArray();
    }

    [Fact]
    public void PartOne()
    {
        var sum = _numbers
            .Where(IsOrdered)
            .Sum(x => int.Parse(x[x.Length / 2]));
        
        Assert.Equal(5268, sum);
    }
    
    [Fact]
    public void PartTwo()
    {
        var sum = _numbers
            .Where(x => !IsOrdered(x))
            .Select(x => x.Order(_comparer).ToArray())
            .Sum(x => int.Parse(x[x.Length / 2]));

        Assert.Equal(5799, sum);
    }

    private bool IsOrdered(string[] items) => items.SequenceEqual(items.Order(_comparer));

    private static IComparer<string> CreateComparer(string[] rules) => Comparer<string>
        .Create((x, y) => rules.AsSpan().Contains($"{x}|{y}") ? - 1 : 1);
}
