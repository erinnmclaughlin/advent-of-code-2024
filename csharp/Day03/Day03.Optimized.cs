using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day03;

public partial class Day03
{
    [Fact]
    [Benchmark]
    public void Part01_Optimized()
    {
        var sum = 0;
        var span = _fileText.AsSpan();

        for (var i = 0; i < span.Length; i++)
        {
            if (span[i..].StartsWith("mul("))
                GetProduct(ref span, ref i, ref sum);
        }
        
        Assert.Equal(161085926, sum);
    }
    
    [Fact]
    [Benchmark]
    public void Part02_Optimized()
    {
        var sum = 0;
        var enabled = true;
        var span = _fileText.AsSpan();

        for (var i = 0; i < span.Length; i++)
        {
            if (span[i..].StartsWith("do()"))
            {
                enabled = true;
                i += 3;
            }
            else if (span[i..].StartsWith("don't()"))
            {
                enabled = false;
                i += 4;
            }
            else if (enabled && span[i..].StartsWith("mul("))
            {
                GetProduct(ref span, ref i, ref sum);
            }
        }
        
        Assert.Equal(82045421, sum);
    }

    private static void GetProduct(ref ReadOnlySpan<char> span, ref int i, ref int sum)
    {
        // skip "mul("
        var end = i += 4;
        
        // find next comma
        while (span[end] != ',') end++;
                
        // check if the content is a valid number
        if (!int.TryParse(span[i..end], out var num1))
            return;
                
        // skip ','
        i = ++end;
                
        // find next closing paren
        while (span[end] != ')') end++;

        // check if the content is a valid number
        if (!int.TryParse(span[i..end], out var num2))
            return;

        i = end;
        sum += num1 * num2;
    }
}