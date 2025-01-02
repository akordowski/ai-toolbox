namespace AIToolbox.Options.SemanticKernel;

public class DefaultPromptExecutionOptions
{
    /// <summary>
    /// Model identifier. This identifies the AI model these settings are configured for e.g.,
    /// gpt-4, gpt-3.5-turbo.
    /// </summary>
    public string? ModelId { get; set; }

    /// <summary>
    /// Service identifier. This identifies the service these settings are configured for e.g.,
    /// azure_openai_eastus, openai, ollama, huggingface, etc.
    /// </summary>
    public string? ServiceId { get; set; }
}
