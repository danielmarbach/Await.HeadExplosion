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
        Add(Job.ShortRun.With(CsProjCoreToolchain.From(new NetCoreAppSettings("netcoreapp2.1", "2.1.0-preview3-26327-02", "2.1-preview3"))));
    }
}