using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Running;

var config = ManualConfig
    .CreateMinimumViable()
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddExporter(MarkdownExporter.GitHub)
    .WithArtifactsPath("..")
    .WithOptions(ConfigOptions.DisableLogFile)
    .WithOptions(ConfigOptions.StopOnFirstError)
    ;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
