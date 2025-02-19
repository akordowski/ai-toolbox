namespace AIToolbox.Options.SemanticKernel;

public sealed class MistralAIOptions
{
    public MistralAIChatCompletionOptions? ChatCompletion { get; set; }
    public MistralAITextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
}
