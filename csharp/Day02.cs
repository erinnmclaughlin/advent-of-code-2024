namespace AoC.CSharp;

public static class Day02
{
    public static int PartOne(string[] fileLines) => fileLines
        .Select(l => l.Split(' ').Select(int.Parse).ToArray())
        .Count(IsSafe);

    public static int PartTwo(string[] fileLines) => fileLines
        .Select(l => l.Split(' ').Select(int.Parse).ToArray())
        .Count(levels => IsSafe(levels) || levels.Any((_, i) => IsSafeWithoutElement(levels, i)));
    
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
