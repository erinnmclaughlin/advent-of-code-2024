using System.Runtime.CompilerServices;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using Xunit.Sdk;

namespace AdventOfCode2024;

public static class BenchmarkRunner
{
    public static string Run<T>([CallerFilePath] string callerFilePath = "") where T : class
    {
        var config = ManualConfig.Create(DefaultConfig.Instance).CreateImmutableConfig();
        var result = BenchmarkDotNet.Running.BenchmarkRunner.Run<T>(config);

        if (result.BenchmarksCases.Length == 0)
            throw new XunitException("There are no benchmarks found.");

        var logger = new AccumulationLogger();
        MarkdownExporter.Console.ExportToLog(result, logger);
        ConclusionHelper.Print(logger, config.GetCompositeAnalyser().Analyse(result).ToList());

        var log = logger.GetLog();

        var filePath = Path.Combine(Path.GetDirectoryName(callerFilePath) ?? callerFilePath, "benchmarks.txt");

        if (File.Exists(filePath))
            File.Delete(filePath);
        
        using var outputFile = File.OpenWrite(filePath);
        using var fileWriter = new StreamWriter(outputFile);
        fileWriter.Write(log);

        return log;
    }
}
