using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

public class Config : ManualConfig
{
    public Config()
    {
        Add(MarkdownExporter.GitHub);
        Add(new MemoryDiagnoser());
        Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp20));
    }
}