using System.Numerics;
using Xunit.Abstractions;

namespace AoC;

public class Day11(ITestOutputHelper output)
{
    private readonly string _fileText = File.ReadAllText("day11.txt");

    [Fact]
    public void PartOne()
    {
        BigInteger sum = 0;

        foreach (var item in _fileText.Split(' '))
            sum += ProcessItem(item, 25);
        
        Assert.Equal(198089, sum);
    }
    
    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 4)]
    [InlineData(3, 5)]
    [InlineData(4, 9)]
    [InlineData(6, 22)]
    [InlineData(25, 55312)]
    //[InlineData(75, 5)]
    public void PartOneExample(int stepCount, BigInteger expected)
    {
        BigInteger sum = 0;
        
        foreach (var part in "125 17".Split(' '))
        {
            sum += ProcessItem(part, stepCount);
        }
        
        Assert.Equal(expected, sum);
    }
    
    private static BigInteger ProcessItem(
        ReadOnlySpan<char> input, 
        in int numberOfSteps, 
        int currentStepNumber = 0)
    {
        if (currentStepNumber++ == numberOfSteps)
            return 1;

        if (input.Length == 0 || input[0] == '0')
        {
            return ProcessItem("1", numberOfSteps, currentStepNumber);
        }

        if (input.Length % 2 == 0)
        {
            var nextSplit = input.Length / 2;
            var left = input[..nextSplit];
            var right = input[nextSplit..].TrimStart('0');

            if (right.Length == 0)
                right = "0";
            
            return ProcessItem(left, numberOfSteps, currentStepNumber) +
                   ProcessItem(right, numberOfSteps, currentStepNumber);
        }

        var number = BigInteger.Multiply(2024, BigInteger.Parse(input)).ToString();
        return ProcessItem(number, numberOfSteps, currentStepNumber);
    }
}