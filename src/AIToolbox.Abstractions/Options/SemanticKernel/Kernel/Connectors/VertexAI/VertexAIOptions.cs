namespace AIToolbox.Options.SemanticKernel;

public sealed class VertexAIOptions
{
    public VertexAIChatCompletionOptions? ChatCompletion { get; set; }
    public VertexAIEmbeddingGenerationOptions? EmbeddingGeneration { get; set; }
}
