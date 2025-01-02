namespace AIToolbox.Options;

public sealed class ClassOptions
{
    /// <summary>
    /// The assembly-qualified name of the class.
    /// </summary>
    public string? AssemblyQualifiedName { get; set; }

    /// <summary>
    /// The type of the class.
    /// </summary>
    public Type? Type { get; set; }
}
