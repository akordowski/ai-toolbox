namespace AIToolbox.Options.SemanticKernel;

public sealed class MistralAIPromptExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// The API version to use.
    /// </summary>
    public string ApiVersion { get; set; } = default!;

    /// <summary>
    /// Default: null The maximum number of tokens to generate in the completion.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Default: null The seed to use for random sampling. If set, different calls will generate
    /// deterministic results.
    /// </summary>
    public int? RandomSeed { get; set; }

    /// <summary>
    /// Default: false Whether to inject a safety prompt before all conversations.
    /// </summary>
    public bool SafePrompt { get; set; }

    /// <summary>
    /// Default: 0.7 What sampling temperature to use, between 0.0 and 1.0. Higher values like 0.8
    /// will make the output more random, while lower values like 0.2 will make it more focused and
    /// deterministic.
    /// </summary>
    public double Temperature { get; set; } = 0.7;

    /// <summary>
    /// Gets or sets the behavior for how tool calls are handled.
    /// </summary>
    public MistralAIToolCallBehavior? ToolCallBehavior { get; set; }

    /// <summary>
    /// Default: 1 Nucleus sampling, where the model considers the results of the tokens with top_p
    /// probability mass.So 0.1 means only the tokens comprising the top 10% probability mass are
    /// considered.
    /// </summary>
    public double TopP { get; set; } = 1;
}
