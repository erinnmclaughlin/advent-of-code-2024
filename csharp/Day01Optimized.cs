namespace AoC.CSharp;

public static class Day01Optimized
{
    public static int PartOne(string[] fileLines)
    {
        Span<int> left = new int[fileLines.Length];
        Span<int> right = new int[fileLines.Length];

        for (var i = 0; i < fileLines.Length; i++)
        {
            var span = fileLines[i].AsSpan();
            var splitIndex = span.IndexOf("   ");
            left[i] = int.Parse(span[..splitIndex]);
            right[i] = int.Parse(span[(splitIndex + 3)..]);
        }

        left.Sort();
        right.Sort();

        var sum = 0;
        for (var i = 0; i < fileLines.Length; i++)
            sum += Math.Abs(left[i] - right[i]);

        return sum;
    }
    
    public static int PartTwo(string[] fileLines)
    {
        Span<int> left = new int[fileLines.Length];
        Span<int> right = new int[fileLines.Length];

        for (var i = 0; i < fileLines.Length; i++)
        {
            var span = fileLines[i].AsSpan();
            var splitIndex = span.IndexOf("   ");
            left[i] = int.Parse(span[..splitIndex]);
            right[i] = int.Parse(span[(splitIndex + 3)..]);
        }

        var sum = 0;
        foreach (var l in left)
            sum += l * right.Count(l);

        return sum;
    }
}
