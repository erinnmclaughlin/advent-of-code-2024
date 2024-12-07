namespace AoC.CSharp;

public static class Day07Optimized
{
    public static long PartOne(string[] fileLines) => fileLines
        .AsParallel()
        .Sum(line =>
        {
            var parts = line.Split(": ");
            var expected = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();
            return GetPossibleOutcomes(expected, numbers, allowConcat: false).FirstOrDefault(o => o == expected);
        });

    public static long PartTwo(string[] fileLines) => fileLines
        .AsParallel()
        .Sum(line =>
        {
            var parts = line.Split(": ");
            var expected = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();
            return GetPossibleOutcomes(expected, numbers, allowConcat: true).FirstOrDefault(o => o == expected);
        });

    private static IEnumerable<long> GetPossibleOutcomes(long maxValue, long[] values, bool allowConcat)
    {
        if (values.Length == 0)
            yield break;

        // we've exceeded the max value there's no need to keep going
        if (values[0] > maxValue)
            yield break;

        if (values.Length == 1)
        {
            yield return values[0];
            yield break;
        }

        var (current, next) = (values[0], values[1]);
        values = values[1..];
        
        // multiply (do this first to find exceeded max values earlier)
        values[0] = current * next;
        foreach (var r in GetPossibleOutcomes(maxValue, values, allowConcat)) yield return r;
        
        // concat (do this second, assuming concat is more likely (I think?) to result in a higher number than add)
        if (allowConcat)
        {
            values[0] = long.Parse($"{current}{next}");
            foreach (var r in GetPossibleOutcomes(maxValue, values, allowConcat)) yield return r;
        }
        
        // add
        values[0] = current + next;
        foreach (var r in GetPossibleOutcomes(maxValue, values, allowConcat)) yield return r;
    }
}