namespace AoC.CSharp;

public static class Day07
{
    public static long PartOne(string[] fileLines) => fileLines
        .AsParallel()
        .Sum(line =>
        {
            var (expected, numbers) = ParseLine(line);
            return GetPossibleOutcomes(expected, numbers, allowConcat: false).FirstOrDefault(o => o == expected);
        });

    public static long PartTwo(string[] fileLines) => fileLines
        .AsParallel()
        .Sum(line =>
        {
            var (expected, numbers) = ParseLine(line);
            return GetPossibleOutcomes(expected, numbers, allowConcat: true).FirstOrDefault(o => o == expected);
        });

    private static (long, long[]) ParseLine(string line)
    {
        var parts = line.Split(": ");
        var expected = long.Parse(parts[0]);
        var numbers = parts[1].Split(' ').Select(long.Parse).ToArray();
        return (expected, numbers);
    }
    
    private static IEnumerable<long> GetPossibleOutcomes(long maxValue, long[] values, bool allowConcat)
    {
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
        
        // doing this in the order that's most likely to grow faster (so we will break out sooner)
        
        // multiply
        values[0] = current * next;
        foreach (var r in GetPossibleOutcomes(maxValue, values, allowConcat)) 
            yield return r;
        
        // concat
        if (allowConcat)
        {
            values[0] = long.Parse($"{current}{next}");
            foreach (var r in GetPossibleOutcomes(maxValue, values, allowConcat)) 
                yield return r;
        }
        
        // add
        values[0] = current + next;
        foreach (var r in GetPossibleOutcomes(maxValue, values, allowConcat)) 
            yield return r;
    }
}