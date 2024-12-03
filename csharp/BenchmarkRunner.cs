using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;

namespace AdventOfCode2024;

public static class BenchmarkRunner
{
    private static readonly IConfig Config = ManualConfig.Create(DefaultConfig.Instance);
    
    public static string Run<T>() where T : class
    {
        var result = BenchmarkDotNet.Running.BenchmarkRunner.Run<T>(Config);

        var logger = new AccumulationLogger();
        MarkdownExporter.Console.ExportToLog(result, logger);
        ConclusionHelper.Print(logger, result.BenchmarksCases.First().Config.GetCompositeAnalyser().Analyse(result).ToList());

        return logger.GetLog();
    }
}