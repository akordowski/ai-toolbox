namespace AIToolbox.Options.SemanticKernel;

public sealed class DirectoryPluginOptions
{
    /// <summary>
    /// Path to the directory containing the plugin.
    /// </summary>
    public string PluginDirectory { get; set; } = default!;

    /// <summary>
    /// The name of the plugin. If null, the name is derived from the <see cref="PluginDirectory"/>
    /// directory name.
    /// </summary>
    public string? PluginName { get; set; }
}
