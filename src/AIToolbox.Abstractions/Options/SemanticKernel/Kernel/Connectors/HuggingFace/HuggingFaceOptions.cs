namespace AIToolbox.Options.SemanticKernel;

public sealed class HuggingFaceOptions
{
    public HuggingFaceChatCompletionOptions? ChatCompletion { get; set; }
    public HuggingFaceImageToTextOptions? ImageToText { get; set; }
    public HuggingFaceTextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
    public HuggingFaceTextGenerationOptions? TextGeneration { get; set; }
}
