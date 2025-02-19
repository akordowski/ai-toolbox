namespace AIToolbox.Options.SemanticKernel;

public sealed class GoogleOptions
{
    public GoogleAIChatCompletionOptions? ChatCompletion { get; set; }
    public GoogleAIEmbeddingGenerationOptions? EmbeddingGeneration { get; set; }
}
