using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using Xunit.Abstractions;

namespace AdventOfCode2024.Day03;

[MemoryDiagnoser]
public partial class Day03(ITestOutputHelper? output = null)
{
    private readonly string _fileText = File.ReadAllText("Day03\\input.txt");

    [BenchmarkRunner]
    public void Benchmarks()
    {
        var summary = BenchmarkRunner.Run<Day03>();
        output?.WriteLine(summary);
    }

    [Fact]
    [Benchmark]
    public void Part01()
    {
        var sum = Regex
            .Matches(_fileText, @"mul\((\d+),(\d+)\)")
            .Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
        
        Assert.Equal(161085926, sum);
    }
    
    [Fact]
    [Benchmark]
    public void Part02()
    {
        var (_, sum) = Regex
            .Matches(_fileText, @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")
            .Aggregate((IsMatchEnabled: true, Sum: 0), (x, match) =>
            {
                if (match.Value.StartsWith("mul") && x.IsMatchEnabled)
                    return (true, x.Sum + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
                
                return (match.Value == "do()", x.Sum);
            });
        
        Assert.Equal(82045421, sum);
    }
}
