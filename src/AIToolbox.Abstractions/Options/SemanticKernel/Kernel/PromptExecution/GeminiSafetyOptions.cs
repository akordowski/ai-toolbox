namespace AIToolbox.Options.SemanticKernel;

public sealed class GeminiSafetyOptions
{
    /// <summary>
    /// Gets or sets the safety category. Default 'Unspecified'.
    /// </summary>
    public GeminiSafetyCategory Category { get; set; } = GeminiSafetyCategory.Unspecified;

    /// <summary>
    /// Gets or sets the safety threshold. Default 'BlockNone'.
    /// </summary>
    public GeminiSafetyThreshold Threshold { get; set; } = GeminiSafetyThreshold.BlockNone;
}
