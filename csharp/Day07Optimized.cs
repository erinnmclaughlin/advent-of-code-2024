namespace AoC.CSharp;

public static class Day07Optimized
{
    public static long PartOne(string[] fileLines)
    {
        long sum = 0;

        foreach (var line in fileLines)
        {
            var parts = line.Split(": ");
            var expected = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();
            sum += GetPossibleOutcomes(expected, numbers, allowConcat: false).FirstOrDefault(o => o == expected);
        }

        return sum;
    }

    public static long PartTwo(string[] fileLines)
    {
        long sum = 0;

        foreach (var line in fileLines)
        {
            var parts = line.Split(": ");
            var expected = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();
            sum += GetPossibleOutcomes(expected, numbers, allowConcat: true).FirstOrDefault(o => o == expected);
        }

        return sum;
    }

    private static IEnumerable<long> GetPossibleOutcomes(long expected, long[] values, bool allowConcat)
    {
        if (values.Length == 0)
            yield break;

        if (values.Length == 1)
        {
            yield return values[0];
            yield break;
        }

        // at this point there's no way we're going to find a match for the expected value, so stop searching
        if (values[0] > expected)
            yield break;
        
        var (current, next) = (values[0], values[1]);
        values = values[1..];
        
        // add
        values[0] = current + next;
        foreach (var r in GetPossibleOutcomes(expected, values, allowConcat)) yield return r;
        
        // multiply
        values[0] = current * next;
        foreach (var r in GetPossibleOutcomes(expected, values, allowConcat)) yield return r;
        
        // concat
        if (!allowConcat)
            yield break;

        values[0] = long.Parse($"{current}{next}");
        foreach (var r in GetPossibleOutcomes(expected, values, allowConcat)) yield return r;
    }
}