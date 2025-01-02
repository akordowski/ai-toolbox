namespace AIToolbox.Options.SemanticKernel;

public enum GeminiSafetyThreshold
{
    /// <summary>
    /// Block when low, medium or high probability of unsafe content.
    /// </summary>
    BlockLowAndAbove,

    /// <summary>
    /// Block when medium or high probability of unsafe content.
    /// </summary>
    BlockMediumAndAbove,

    /// <summary>
    /// Always show regardless of probability of unsafe content.
    /// </summary>
    BlockNone,

    /// <summary>
    /// Block when high probability of unsafe content.
    /// </summary>
    BlockOnlyHigh,

    /// <summary>
    /// Threshold is unspecified, block using default threshold.
    /// </summary>
    Unspecified
}
