namespace AdventOfCode2024.Day01;

public static class Solution
{
    public static int PartOne(string[] fileLines)
    {
        var (left, right) = ParseFile(fileLines);
        
        return left
            .Order()
            .Zip(right.Order().ToArray(), (l, r) => Math.Abs(l - r))
            .Sum();
    }
    
    public static int PartTwo(string[] fileLines)
    {
        var (left, right) = ParseFile(fileLines);
        return left.Sum(l => l * right.Count(r => r == l));
    }

    private static (int[] Left, int[] Right) ParseFile(string[] lines)
    {
        var (left, right) = (new int[lines.Length], new int[lines.Length]);

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split("   ");
            (left[i], right[i]) = (int.Parse(parts[0]), int.Parse(parts[1]));
        }

        return (left, right);
    }
}
