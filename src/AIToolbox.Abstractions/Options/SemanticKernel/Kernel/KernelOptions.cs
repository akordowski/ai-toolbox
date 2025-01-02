namespace AIToolbox.Options.SemanticKernel;

public sealed class KernelOptions
{
    public bool AddLogging { get; set; }
    public KernelConnectorOptions? Connectors { get; set; }
    public PluginOptions? Plugins { get; set; }
}
