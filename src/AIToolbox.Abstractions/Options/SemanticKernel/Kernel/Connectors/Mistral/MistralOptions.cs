namespace AIToolbox.Options.SemanticKernel;

public sealed class MistralOptions
{
    public MistralChatCompletionOptions? ChatCompletion { get; set; }
    public MistralTextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
}
