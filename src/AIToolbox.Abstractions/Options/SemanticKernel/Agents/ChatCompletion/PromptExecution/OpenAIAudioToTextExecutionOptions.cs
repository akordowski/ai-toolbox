namespace AIToolbox.Options.SemanticKernel;

public sealed class OpenAIAudioToTextExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// Filename or identifier associated with audio data. Should be in format <c>{filename}.{extension}</c>.
    /// </summary>
    public string Filename { get; set; } = default!;

    /// <summary>
    /// An optional language of the audio data as two-letter ISO-639-1 language code (e.g. 'en' or 'es').
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// An optional text to guide the model's style or continue a previous audio segment. The prompt
    /// should match the audio language.
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// The format of the transcript output, in one of these options: json, text, srt, verbose_json,
    /// or vtt. Default is 'json'.
    /// </summary>
    public string ResponseFormat { get; set; } = "json";

    /// <summary>
    /// The sampling temperature, between 0 and 1. Higher values like 0.8 will make the output more
    /// random, while lower values like 0.2 will make it more focused and deterministic. If set to 0,
    /// the model will use log probability to automatically increase the temperature until certain
    /// thresholds are hit. Default is 0.
    /// </summary>
    public float Temperature { get; set; }
}
