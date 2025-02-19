namespace AIToolbox.Options.SemanticKernel;

public sealed class OpenAIOptions
{
    public OpenAIAudioToTextOptions? AudioToText { get; set; }
    public OpenAIChatCompletionOptions? ChatCompletion { get; set; }
    public OpenAIFilesOptions? Files { get; set; }
    public OpenAITextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
    public OpenAITextToAudioOptions? TextToAudio { get; set; }
    public OpenAITextToImageOptions? TextToImage { get; set; }
}
