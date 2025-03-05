namespace AIToolbox.Options.SemanticKernel;

public sealed class WeaviateMemoryStoreOptions
{
    public string Endpoint { get; set; } = default!;
    public string? ApiKey { get; set; }
    public string? ApiVersion { get; set; }
}
