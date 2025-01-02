namespace AIToolbox.Options.SemanticKernel;

public sealed class AzureOpenAIPromptExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// The system prompt to use when generating text using a chat model. Defaults to "Assistant is
    /// a large language model."
    /// </summary>
    public string? ChatSystemPrompt { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing
    /// frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.
    /// </summary>
    public double FrequencyPenalty { get; set; }

    /// <summary>
    /// Whether to return log probabilities of the output tokens or not. If true, returns the log
    /// probabilities of each output token returned in the <c>content</c> of <c>message</c>.
    /// </summary>
    public bool? Logprobs { get; set; }

    /// <summary>
    /// The maximum number of tokens to generate in the completion.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they
    /// appear in the text so far, increasing the model's likelihood to talk about new topics.
    /// </summary>
    public double PresencePenalty { get; set; }

    /// <summary>
    /// Gets or sets the response format to use for the completion.
    /// </summary>
    /// <remarks>Possible values are: "json_object", "text".</remarks>
    public object? ResponseFormat { get; set; }

    /// <summary>
    /// If specified, the system will make a best effort to sample deterministically such that
    /// repeated requests with the same seed and parameters should return the same result.
    /// Determinism is not guaranteed.
    /// </summary>
    public long? Seed { get; set; }

    /// <summary>
    /// Sequences where the completion will stop generating further tokens.
    /// </summary>
    public IList<string>? StopSequences { get; set; }

    /// <summary>
    /// Temperature controls the randomness of the completion. The higher the temperature, the more
    /// random the completion. Default is 1.0.
    /// </summary>
    public double Temperature { get; set; } = 1.0;

    /// <summary>
    /// Modify the likelihood of specified tokens appearing in the completion.
    /// </summary>
    public IDictionary<int, int>? TokenSelectionBiases { get; set; }

    /// <summary>
    /// Gets or sets the behavior for how tool calls are handled.
    /// </summary>
    public ToolCallBehavior? ToolCallBehavior { get; set; }

    /// <summary>
    /// An integer specifying the number of most likely tokens to return at each token position,
    /// each with an associated log probability.
    /// </summary>
    public int? TopLogprobs { get; set; }

    /// <summary>
    /// TopP controls the diversity of the completion. The higher the TopP, the more diverse the
    /// completion. Default is 1.0.
    /// </summary>
    public double TopP { get; set; } = 1.0;

    /// <summary>
    /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
    /// </summary>
    public string? User { get; set; }
}
