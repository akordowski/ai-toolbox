namespace AIToolbox.Options.SemanticKernel;

public sealed class MemoryOptions
{
    public MemoryStoreOptions? Store { get; set; }
    public TextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
}
