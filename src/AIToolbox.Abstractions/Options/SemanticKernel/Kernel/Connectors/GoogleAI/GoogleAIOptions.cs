namespace AIToolbox.Options.SemanticKernel;

public sealed class GoogleAIOptions
{
    public GoogleAIChatCompletionOptions? ChatCompletion { get; set; }
    public GoogleAIEmbeddingGenerationOptions? EmbeddingGeneration { get; set; }
}
