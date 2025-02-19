namespace AIToolbox.Options.SemanticKernel;

public sealed class PluginOptions
{
    /// <summary>
    /// Get an enumeration of <see cref="AssemblyPluginOptions"/>.
    /// </summary>
    public IEnumerable<AssemblyPluginOptions> AssemblyPlugins { get; set; } = [];

    /// <summary>
    /// Get an enumeration of <see cref="DirectoryPluginOptions"/>.
    /// </summary>
    public IEnumerable<DirectoryPluginOptions> DirectoryPlugins { get; set; } = [];

    /// <summary>
    /// Get an enumeration of <see cref="TypePluginOptions"/>.
    /// </summary>
    public IEnumerable<TypePluginOptions> TypePlugins { get; set; } = [];
}
