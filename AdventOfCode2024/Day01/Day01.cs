namespace AdventOfCode2024.Day01;

public sealed class Day01
{
    [Fact]
    public void PartOne()
    {
        var (left, right) = ProcessFile();
        var answer = left.Select((l, i) => Math.Abs(l - right[i])).Sum();
        Assert.Equal(1879048, answer);
    }
    
    [Fact]
    public void PartTwo()
    {
        var (left, right) = ProcessFile();
        var answer = left.Select(l => l * right.Count(r => r == l)).Sum();
        Assert.Equal(21024792, answer);
    }

    private static (int[] Left, int[] Right) ProcessFile()
    {
        var lines = File.ReadAllLines(Path.Combine("Day01", "input.txt"));
        var (left, right) = (new int[lines.Length], new int[lines.Length]);

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split("   ");
            (left[i], right[i]) = (int.Parse(parts[0]), int.Parse(parts[1]));
        }

        return (left.Order().ToArray(), right.Order().ToArray());
    }
}