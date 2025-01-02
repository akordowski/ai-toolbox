namespace AIToolbox.Options.SemanticKernel;

public sealed class OllamaPromptExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// Sets the stop sequences to use. When this pattern is encountered the
    /// LLM will stop generating text and return. Multiple stop patterns may
    /// be set by specifying multiple separate stop parameters in a model file.
    /// </summary>
    public List<string>? Stop { get; set; }

    /// <summary>
    /// The temperature of the model. Increasing the temperature will make the
    /// model answer more creatively. (Default: 0.8)
    /// </summary>
    public float? Temperature { get; set; }

    /// <summary>
    /// Reduces the probability of generating nonsense. A higher value
    /// (e.g. 100) will give more diverse answers, while a lower value (e.g. 10)
    /// will be more conservative. (Default: 40)
    /// </summary>
    public int? TopK { get; set; }

    /// <summary>
    /// Works together with top-k. A higher value (e.g., 0.95) will lead to
    /// more diverse text, while a lower value (e.g., 0.5) will generate more
    /// focused and conservative text. (Default: 0.9)
    /// </summary>
    public float? TopP { get; set; }
}
