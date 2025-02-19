namespace AIToolbox.Options.SemanticKernel;

public sealed class KernelOptions
{
    /// <summary>
    /// Gets <see cref="ConnectorOptions"/>.
    /// </summary>
    public ConnectorOptions? Connectors { get; set; }

    /// <summary>
    /// Gets <see cref="PluginOptions"/>.
    /// </summary>
    public PluginOptions? Plugins { get; set; }
}
