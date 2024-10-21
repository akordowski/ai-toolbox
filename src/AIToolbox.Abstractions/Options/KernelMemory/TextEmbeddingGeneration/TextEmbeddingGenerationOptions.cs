namespace AIToolbox.Options.KernelMemory;

public sealed class TextEmbeddingGenerationOptions
{
    public AzureOpenAITextEmbeddingGenerationOptions? AzureOpenAI { get; set; }
    public OpenAITextEmbeddingGenerationOptions? OpenAI { get; set; }
    public SemanticKernelTextEmbeddingGenerationOptions? SemanticKernel { get; set; }
    public CustomEmbeddingGeneratorOptions? Custom { get; set; }
}
