namespace AIToolbox.Options.SemanticKernel;

public sealed class AssemblyPluginOptions
{
    /// <summary>
    /// The assembly-qualified name of the plugin.
    /// </summary>
    public string AssemblyQualifiedName { get; set; } = default!;

    /// <summary>
    /// The name of the plugin. If the value is null, a plugin name is derived from the type of the
    /// <see cref="AssemblyQualifiedName"/>.
    /// </summary>
    public string? PluginName { get; set; }
}
