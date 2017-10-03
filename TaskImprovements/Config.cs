using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

public class Config : ManualConfig
{
    public Config()
    {
        Add(MarkdownExporter.GitHub);
        Add(new MemoryDiagnoser());
        Add(Job.Default.With(CsProjCoreToolchain.From(new NetCoreAppSettings("netcoreapp2.1", "2.1.0-preview1-25719-04"))));
    }
}