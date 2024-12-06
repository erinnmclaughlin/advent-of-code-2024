namespace AoC.CSharp;

public static class Day05Optimized
{
    public static int PartOne(string[] fileLines)
    { 
        var lines = fileLines.AsSpan();
        var splitIndex = lines.IndexOf(string.Empty);
        var ruleSpan = lines[..splitIndex];
        var numberSpan = lines[(splitIndex + 1)..];
        var sum = 0;

        foreach (var ns in numberSpan)
        {
            var isOrdered = true;
            var numberLine = ns.AsSpan();

            foreach (var rs in ruleSpan)
            {
                var ruleLine = rs.AsSpan();
                var index = ruleLine.IndexOf('|');
                var num1 = ruleLine[..index];
                var num2 = ruleLine[(index + 1)..];

                var num1Index = numberLine.IndexOf(num1);
                if (num1Index == -1) continue;
                
                var num2Index = numberLine.IndexOf(num2);
                if (num2Index == -1) continue;

                if (num1Index < num2Index) continue;
                
                isOrdered = false;
                break;
            }

            if (isOrdered)
            {
                var numberOfNumbers = numberLine.Count(',') + 1;
                var split = numberLine.Split(',');

                for (var j = 0; j <= numberOfNumbers / 2; j++)
                {
                    split.MoveNext();
                }

                var (offset, length) = split.Current.GetOffsetAndLength(numberLine.Length);
                
                var item = numberLine.Slice(offset, length);
                sum += int.Parse(item);
            }
        }

        return sum;

    }
}