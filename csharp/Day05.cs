namespace AoC.CSharp;

public static class Day05
{
    public static int PartOne(string[] fileLines)
    {
        var (numbers, comparer) = ParseFile(fileLines);
        
        return numbers
            .Select(x => x.Split(','))
            .Where(x => IsOrdered(x, comparer))
            .Sum(x => int.Parse(x[x.Length / 2]));
    }
    
    public static int PartTwo(string[] fileLines)
    {
        var (numbers, comparer) = ParseFile(fileLines);
        
        return numbers
            .Select(x => x.Split(','))
            .Where(x => !IsOrdered(x, comparer))
            .Select(x => x.Order(comparer).ToArray())
            .Sum(x => int.Parse(x[x.Length / 2]));
    }

    private static (string[], Comparer<string>) ParseFile(string[] fileLines)
    {
        var lines = fileLines.AsSpan();
        var splitIndex = lines.IndexOf("");
        var numbers = lines[(splitIndex + 1)..].ToArray();
        var comparer = CreateComparer(lines[..splitIndex].ToArray());
        return (numbers, comparer);
    }

    private static bool IsOrdered(string[] items, Comparer<string> comparer)
        => items.SequenceEqual(items.Order(comparer));

    private static Comparer<string> CreateComparer(string[] rules)
        => Comparer<string>.Create((x, y) => rules.AsSpan().Contains($"{x}|{y}") ? - 1 : 1);
}
