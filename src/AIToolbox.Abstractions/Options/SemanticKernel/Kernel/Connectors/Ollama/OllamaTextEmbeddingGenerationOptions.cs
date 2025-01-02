namespace AIToolbox.Options.SemanticKernel;

public sealed class OllamaTextEmbeddingGenerationOptions
{
    public string ModelId { get; set; } = default!;
    public string? Endpoint { get; set; }
    public string? ServiceId { get; set; }
}
