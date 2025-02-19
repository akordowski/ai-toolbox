namespace AIToolbox.Options.SemanticKernel;

public sealed class AzureOpenAIOptions
{
    public AzureOpenAIAudioToTextOptions? AudioToText { get; set; }
    public AzureOpenAIChatCompletionOptions? ChatCompletion { get; set; }
    public AzureOpenAIFilesOptions? Files { get; set; }
    public AzureOpenAITextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
    public AzureOpenAITextGenerationOptions? TextGeneration { get; set; }
    public AzureOpenAITextToAudioOptions? TextToAudio { get; set; }
    public AzureOpenAITextToImageOptions? TextToImage { get; set; }
}
