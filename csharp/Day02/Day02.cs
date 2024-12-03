namespace AdventOfCode2024.Day02;

public sealed class Day02
{
    [Fact]
    public void PartOne()
    {
        var answer = ProcessFile().Count(IsSafe);
        Assert.Equal(407, answer);
    }

    [Fact]
    public void PartTwo()
    {
        var answer = ProcessFile().Count(levels => IsSafe(levels) || levels.Any((_, i) => IsSafeWithoutElement(levels, i)));
        Assert.Equal(459, answer);
    }
    
    private static IEnumerable<int[]> ProcessFile() => File
        .ReadLines(Path.Combine("Day02", "input.txt"))
        .Select(line => line.Split(' ').Select(int.Parse).ToArray());

    private static bool IsSafe(int[] levels)
    {
        var shouldDescend = levels[1] < levels[0];

        for (var i = 0; i < levels.Length - 1; i++)
        {
            var (current, next) = (levels[i], levels[i + 1]);
            if (Math.Abs(next - current) is < 1 or > 3 || shouldDescend != next < current) 
                return false;
        }

        return true;
    }
    
    private static bool IsSafeWithoutElement(int[] levels, int index)
    {
        return IsSafe(levels.Where((_, i) => i != index).ToArray());
    }
}
