namespace AIToolbox.Options.SemanticKernel;

public sealed class PluginOptions
{
    public IEnumerable<AssemblyPluginOptions> AssemblyPlugins { get; set; } = [];
    public IEnumerable<DirectoryPluginOptions> DirectoryPlugins { get; set; } = [];
    public IEnumerable<TypePluginOptions> TypePlugins { get; set; } = [];
}
