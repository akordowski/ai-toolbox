namespace AIToolbox.Options.SemanticKernel;

public sealed class OpenAITextToAudioExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// The format to audio in. Supported formats are mp3, opus, aac, and flac.
    /// </summary>
    public string ResponseFormat { get; set; } = "json";

    /// <summary>
    /// The speed of the generated audio. Select a value from 0.25 to 4.0. 1.0 is the default.
    /// </summary>
    public float Speed { get; set; } = 1;

    /// <summary>
    /// The voice to use when generating the audio. Supported voices are alloy, echo, fable, onyx,
    /// nova, and shimmer.
    /// </summary>
    public string Voice { get; set; } = default!;
}
