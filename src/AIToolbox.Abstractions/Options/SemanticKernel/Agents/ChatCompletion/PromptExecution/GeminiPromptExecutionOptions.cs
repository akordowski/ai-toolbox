namespace AIToolbox.Options.SemanticKernel;

public sealed class GeminiPromptExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// The count of candidates. Possible values range from 1 to 8.
    /// </summary>
    public int? CandidateCount { get; set; }

    /// <summary>
    /// The maximum number of tokens to generate in the completion.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Represents a list of safety settings.
    /// </summary>
    public IList<GeminiSafetyOptions>? SafetySettings { get; set; }

    /// <summary>
    /// Sequences where the completion will stop generating further tokens. Maximum number of stop
    /// sequences is 5.
    /// </summary>
    public IList<string>? StopSequences { get; set; }

    /// <summary>
    /// Temperature controls the randomness of the completion. The higher the temperature, the more
    /// random the completion. Range is 0.0 to 1.0.
    /// </summary>
    public double? Temperature { get; set; }

    /// <summary>
    /// Gets or sets the behavior for how tool calls are handled.
    /// </summary>
    public GeminiToolCallBehavior? ToolCallBehavior { get; set; }

    /// <summary>
    /// Gets or sets the value of the TopK property. The TopK property represents the maximum value
    /// of a collection or dataset.
    /// </summary>
    public int? TopK { get; set; }

    /// <summary>
    /// TopP controls the diversity of the completion. The higher the TopP, the more diverse the completion.
    /// </summary>
    public double? TopP { get; set; }
}
