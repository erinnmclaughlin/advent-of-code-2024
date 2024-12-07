using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC.Extras;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class IndexOfComparisons
{
    private readonly string _fileText = File.ReadAllText("day06.txt");
    
    [Benchmark(Baseline = true)]
    public int String_IndexOf() => _fileText.IndexOf('^');

    [Benchmark]
    public int String_AsSpan_IndexOf() => _fileText.AsSpan().IndexOf('^');

    [Benchmark]
    public int String_AsMemory_Span_IndexOf() => _fileText.AsMemory().Span.IndexOf('^');
    
    [Benchmark]
    public int String_ToCharArray_AsSpan_IndexOf() => _fileText.ToCharArray().AsSpan().IndexOf('^');
    
    [Benchmark]
    public int String_ToArray_AsSpan_IndexOf() => _fileText.ToArray().AsSpan().IndexOf('^');
}
