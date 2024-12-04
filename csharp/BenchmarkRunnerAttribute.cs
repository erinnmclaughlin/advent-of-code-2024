namespace AdventOfCode2024;

public class BenchmarkRunnerAttribute : FactAttribute
{
#if DEBUG
    public override string Skip { get; set; } = "Benchmarks will not run for debug builds.";
#endif
}