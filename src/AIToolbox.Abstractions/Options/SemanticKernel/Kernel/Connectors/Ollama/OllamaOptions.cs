namespace AIToolbox.Options.SemanticKernel;

public sealed class OllamaOptions
{
    public OllamaChatCompletionOptions? ChatCompletion { get; set; }
    public OllamaTextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
    public OllamaTextGenerationOptions? TextGeneration { get; set; }
}
