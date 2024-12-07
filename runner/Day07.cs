using System.Numerics;
using Xunit.Abstractions;

namespace AoC;

public class Day07(ITestOutputHelper output)
{
    private readonly string[] _fileLines = File.ReadAllLines("day07.example.txt");
    //private readonly string[] _fileLines = File.ReadAllLines("day07.txt");

    [Fact]
    public void PartOne()
    {
        BigInteger sum = 0;

        foreach (var line in _fileLines)
        {
            var parts = line.Split(':');
            var expected = BigInteger.Parse(parts[0]);
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
        
        output.WriteLine(sum.ToString());
    }
    
    private static IEnumerable<BigInteger> GetPossibleOutcomes(string numberString)
    {
        var numbers = numberString.Split(' ').Select(BigInteger.Parse).ToArray();

        if (numbers.Length == 1)
        {
            yield return numbers[0];
        }
        else
        {
            foreach (var r in GetPossibleValues(numbers[0], numbers[1..]))
                yield return r;
        }
    }

    private static IEnumerable<BigInteger> GetPossibleValues(BigInteger value, BigInteger[] nextValues)
    {
        while (true)
        {
            var nextValue = nextValues[0];

            var addResult = value + nextValue;
            var multiplyResult = value * nextValue;

            if (nextValues.Length == 1)
            {
                yield return addResult;
                yield return multiplyResult;
            }
            else
            {
                foreach (var r in GetPossibleValues(addResult, nextValues[1..])) yield return r;

                value = multiplyResult;
                nextValues = nextValues[1..];
                continue;
            }

            break;
        }
    }
}