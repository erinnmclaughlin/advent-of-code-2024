using System.Text;

namespace AoC.CSharp;

public static class Day11
{
    public static long CountStones(string input, int numberOfRounds)
    {
        var stones = input.Split(' ').Select(n => new Stone { Number = n }).ToList();

        for (var i = 0; i < numberOfRounds; i++)
        {
            stones = stones
                .AsParallel()
                .SelectMany(Blink)
                .GroupBy(s => s.Number)
                .Select(s =>
                {
                    var stone = s.First();
                    stone.Count += s.Skip(1).Sum(x => x.Count);
                    return stone;
                })
                .ToList();
        }

        return stones.Sum(s => s.Count);
    }

    private static IEnumerable<Stone> Blink(Stone stone)
    {
        if (stone.Number == "0")
        {
            stone.Number = "1";
        }
        else if (stone.Number.Length % 2 == 0)
        {
            yield return new Stone
            {
                Number = stone.Number[..(stone.Number.Length / 2)],
                Count = stone.Count
            };
                
            stone.Number = stone.Number[(stone.Number.Length / 2)..].TrimStart('0');
            if (stone.Number.Length == 0) stone.Number = "0";
        }
        else
        {
            stone.Number = (long.Parse(stone.Number) * 2024).ToString();
        }

        yield return stone;
    }
    
    public sealed class Stone
    {
        public required string Number { get; set; }
        public long Count { get; set; } = 1;
    }
    
    public static long CountStonesSlow(ReadOnlySpan<char> span)
    {
        for (var i = 0; i < 25; i++)
        {
            var newSpan = new StringBuilder();
            var split = span.Split(' ');
            while (split.MoveNext())
            {
                var (offset, length) = split.Current.GetOffsetAndLength(span.Length);
                var number = span.Slice(offset, length);

                if (number is "0")
                {
                    newSpan.Append("1 ");
                }
                else if (number.Length % 2 == 0)
                {
                    var n1 = number[..(number.Length / 2)];
                    newSpan.Append(n1).Append(' ');
                    var n2 = number[(number.Length / 2)..].TrimStart('0');
                    newSpan.Append(n2.IsEmpty ? "0" : n2).Append(' ');
                }
                else
                {
                    newSpan.Append(long.Parse(number) * 2024).Append(' ');
                }
            }

            span = newSpan.ToString().AsSpan().TrimEnd(' ');
        }

        return span.ToString().Split(' ').Distinct().Count();
        //return span.Count(' ') + 1;
    }
}