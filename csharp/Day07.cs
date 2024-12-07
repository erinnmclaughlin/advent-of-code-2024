namespace AoC.CSharp;

public static class Day07
{
    public static long PartOne(string[] fileLines)
    {
        long sum = 0;

        foreach (var line in fileLines)
        {
            var parts = line.Split(':');
            var expected = long.Parse(parts[0]);
            var numberString = parts[1].Trim();

            foreach (var outcome in GetPossibleOutcomes(numberString))
            {
                if (outcome == expected)
                {
                    sum += outcome;
                    break;
                }
            }
        }

        return sum;
    }

    public static long PartTwo(string[] fileLines)
    {
        long sum = 0;

        foreach (var line in fileLines)
        {
            var parts = line.Split(':');
            var expected = long.Parse(parts[0]);
            var numberString = parts[1].Trim();

            foreach (var outcome in GetPossibleOutcomes(numberString, true))
            {
                if (outcome == expected)
                {
                    sum += outcome;
                    break;
                }
            }
        }

        return sum;
    }
    
    private static IEnumerable<long> GetPossibleOutcomes(string numberString, bool allowConcat = false)
    {
        var numbers = numberString.Split(' ').Select(long.Parse).ToArray();

        if (numbers.Length == 1)
        {
            yield return numbers[0];
        }
        else
        {
            foreach (var r in GetPossibleOutcomes(numbers[0], numbers[1..], allowConcat))
                yield return r;
        }
    }

    private static IEnumerable<long> GetPossibleOutcomes(long value, long[] nextValues, bool allowConcat = false)
    {
        var nextValue = nextValues[0];
        
        var addResult = value + nextValue;
        var multiplyResult = value * nextValue;
        var concatResult = long.Parse($"{value}{nextValue}");
        
        if (nextValues.Length == 1)
        {
            yield return addResult;
            yield return multiplyResult;
                
            if (allowConcat)
                yield return concatResult;
        }
        else
        {
            foreach (var r in GetPossibleOutcomes(addResult, nextValues[1..], allowConcat)) yield return r;
            foreach (var r in GetPossibleOutcomes(multiplyResult, nextValues[1..], allowConcat)) yield return r;
            
            if (allowConcat)
                foreach (var r in GetPossibleOutcomes(concatResult, nextValues[1..], allowConcat)) yield return r;
        }
    }
}