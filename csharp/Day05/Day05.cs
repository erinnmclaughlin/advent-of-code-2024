using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Day05;

[MemoryDiagnoser]
public class Day05
{
    private readonly string[] _fileLines = File.ReadAllLines(Path.Combine("Day05", "input.txt"));

    [BenchmarkRunner]
    public void Run() => BenchmarkRunner.Run<Day05>();
    
    [Fact, Benchmark]
    public void PartOne()
    {
        var (numbers, comparer) = ParseFile();
        
        var sum = numbers
            .Select(x => x.Split(','))
            .Where(x => IsOrdered(x, comparer))
            .Sum(x => int.Parse(x[x.Length / 2]));
        
        Assert.Equal(5268, sum);
    }
    
    [Fact, Benchmark]
    public void PartTwo()
    {
        var (numbers, comparer) = ParseFile();
        
        var sum = numbers
            .Select(x => x.Split(','))
            .Where(x => !IsOrdered(x, comparer))
            .Select(x => x.Order(comparer).ToArray())
            .Sum(x => int.Parse(x[x.Length / 2]));

        Assert.Equal(5799, sum);
    }

    private (string[], Comparer<string>) ParseFile()
    {
        var lines = _fileLines.AsSpan();
        var splitIndex = lines.IndexOf("");
        var numbers = lines[(splitIndex + 1)..].ToArray();
        var comparer = CreateComparer(lines[..splitIndex].ToArray());
        return (numbers, comparer);
    }

    private static bool IsOrdered(string[] items, Comparer<string> comparer)
        => items.SequenceEqual(items.Order(comparer));

    private static Comparer<string> CreateComparer(string[] rules)
        => Comparer<string>.Create((x, y) => rules.AsSpan().Contains($"{x}|{y}") ? - 1 : 1);
}
