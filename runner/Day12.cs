using Xunit.Abstractions;

namespace AoC;

public class Day12(ITestOutputHelper output)
{
    private readonly string[] _exampleInput = File.ReadAllLines("day12.txt");

    [Fact]
    public void PartOne()
    {
        var sum = FormGroups(_exampleInput)
            .OrderBy(x => x.Cells.Min(c => c.Row))
            .Sum(group => group.Cells.Count * group.GetPerimeter());

        output.WriteLine(sum.ToString());
        Assert.Equal(1465968, sum);
    }

    [Fact]
    public void PartTwo()
    {
    }
    
    private static List<CellGroup> FormGroups(ReadOnlySpan<string> map)
    {
        var groupLookup = new Dictionary<char, List<CellGroup>>();

        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            { 
                var cell = new Cell(map[i][j], i, j);
                groupLookup.TryAdd(cell.Label, []);
                var group = new CellGroup(cell.Label);
                group.Cells.Add(cell);
                groupLookup[cell.Label].Add(group);
            }
        }

        Parallel.ForEach(groupLookup, kvp =>
        {
            for (var i = kvp.Value.Count - 1; i >= 0; i--)
            {
                for (var j = 0; j < i; j++)
                {
                    if (!kvp.Value[i].CanMergeWith(kvp.Value[j]))
                        continue;

                    kvp.Value[i].Cells.AddRange(kvp.Value[j].Cells);
                    kvp.Value.RemoveAt(j);
                    break;
                }
            }
        });

        return groupLookup.SelectMany(x => x.Value).ToList();
    }

    private sealed class CellGroup(char label)
    {
        public char Label { get; } = label;
        public List<Cell> Cells { get; } = [];

        public bool CanMergeWith(CellGroup other)
        {
            return other != this &&
                   other.Label == Label && 
                   Cells.Any(c => other.Cells.Any(oc => oc.IsAdjacentTo(c)));
        }

        public int GetPerimeter() => Cells.Sum(cell => 4 - Cells.Count(c => c.IsAdjacentTo(cell)));
    }

    private sealed class Cell(char label, int row, int col)
    {
        public char Label { get; } = label;
        public int Row { get; } = row;
        public int Col { get; } = col;

        public bool IsAdjacentTo(Cell other) =>
            Row == other.Row && Math.Abs(Col - other.Col) == 1 ||
            Col == other.Col && Math.Abs(Row - other.Row) == 1;
    }
}