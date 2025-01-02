namespace AIToolbox.Options.SemanticKernel;

public sealed class TypePluginOptions
{
    /// <summary>
    /// The type of the plugin class.
    /// </summary>
    public Type PluginType { get; set; } = default!;

    /// <summary>
    /// The name of the plugin. If the value is null, a plugin name is derived from the type of the
    /// <see cref="PluginType"/>.
    /// </summary>
    public string? PluginName { get; set; }
}
