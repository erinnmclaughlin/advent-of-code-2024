﻿namespace AoC.CSharp;

public static class Day03Optimized
{
    public static int PartOne(string fileText)
    {
        var sum = 0;
        var span = fileText.AsSpan();

        for (var i = 0; i < span.Length; i++)
        {
            if (span[i..].StartsWith("mul("))
                CalculateProduct(ref span, ref i, ref sum);
        }

        return sum;
    }
    
    public static int PartTwo(string fileText)
    {
        var sum = 0;
        var enabled = true;
        var span = fileText.AsSpan();

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
                i += 6;
            }
            else if (span[i..].StartsWith("mul("))
            {
                if (enabled)
                {
                    CalculateProduct(ref span, ref i, ref sum);
                }
                else
                {
                    // skip "mul("
                    i += 4;
                    while (i < span.Length && !span[i..].StartsWith("do()")) i++;
                    i--;
                }
            }
        }

        return sum;
    }

    private static void CalculateProduct(ref ReadOnlySpan<char> span, ref int i, ref int sum)
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