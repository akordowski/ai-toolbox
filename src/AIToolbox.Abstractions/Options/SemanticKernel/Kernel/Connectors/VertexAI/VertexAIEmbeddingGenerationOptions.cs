namespace AIToolbox.Options.SemanticKernel;

public sealed class VertexAIEmbeddingGenerationOptions
{
    public string ModelId { get; set; } = default!;
    public string? BearerKey { get; set; }
    public string? Location { get; set; }
    public string? ProjectId { get; set; }
    public VertexAIVersion? ApiVersion { get; set; }
    public string? ServiceId { get; set; }
}
