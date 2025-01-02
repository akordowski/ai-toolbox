namespace AIToolbox.Options.SemanticKernel;

public sealed class HuggingFacePromptExecutionOptions : DefaultPromptExecutionOptions
{
    /// <summary>
    /// Show details of the generation. Including usage.
    /// </summary>
    public bool? Details { get; set; }

    /// <summary>
    /// Whether to return log probabilities of the output tokens or not. If true, returns the log
    /// probabilities of each output token returned in the content of message.
    /// </summary>
    public bool? LogProbs { get; set; }

    /// <summary>
    /// Int (0-250). The amount of new tokens to be generated, this does not include the input
    /// length it is an estimate of the size of generated text you want. Each new tokens slows
    /// down the request, so look for balance between response times and length of text generated.
    /// </summary>
    public int? MaxNewTokens { get; set; }

    /// <summary>
    /// (Default: None). Float (0-120.0). The amount of time in seconds that the query should take
    /// maximum. Network can cause some overhead, so it will be a soft limit. Use that in combination
    /// with max_new_tokens for best results.
    /// </summary>
    public float? MaxTime { get; set; }

    /// <summary>
    /// The maximum number of tokens to generate in the completion.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they
    /// appear in the text so far, increasing the model's likelihood to talk about new topics.
    /// </summary>
    public float? PresencePenalty { get; set; }

    /// <summary>
    /// (Default: None). Float (0.0-100.0). The more a token is used within generation the more it
    /// is penalized to not be picked in successive generation passes.
    /// </summary>
    public float? RepetitionPenalty { get; set; }

    /// <summary>
    /// (Default: 1). Integer. The number of proposition you want to be returned.
    /// </summary>
    public int ResultsPerPrompt { get; set; } = 1;

    /// <summary>
    /// The seed to use for generating a similar output.
    /// </summary>
    public long? Seed { get; set; }

    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens.
    /// </summary>
    public List<string>? Stop { get; set; }

    /// <summary>
    /// (Default: 1.0). Float (0.0-100.0). The temperature of the sampling operation. 1 means regular
    /// sampling, 0 means always take the highest score, 100.0 is getting closer to uniform probability.
    /// </summary>
    public float Temperature { get; set; } = 1;

    /// <summary>
    /// (Default: None). Integer to define the top tokens considered within the sample operation to
    /// create new text.
    /// </summary>
    public int? TopK { get; set; }

    /// <summary>
    /// An integer between 0 and 5 specifying the number of most likely tokens to return at each
    /// token position, each with an associated log probability. logprobs must be set to true if
    /// this parameter is used.
    /// </summary>
    public int? TopLogProbs { get; set; }

    /// <summary>
    /// (Default: None). Float to define the tokens that are within the sample operation of text
    /// generation. Add tokens in the sample for more probable to least probable until the sum of
    /// the probabilities is greater than top_p.
    /// </summary>
    public float? TopP { get; set; }

    /// <summary>
    /// (Default: true). Boolean. There is a cache layer on the inference API to speedup requests
    /// we have already seen. Most models can use those results as is as models are deterministic
    /// (meaning the results will be the same anyway). However, if you use a non-deterministic model,
    /// you can set this parameter to prevent the caching mechanism from being used resulting in a
    /// real new query.
    /// </summary>
    public bool UseCache { get; set; } = true;

    /// <summary>
    /// (Default: false) Boolean. If the model is not ready, wait for it instead of receiving 503.
    /// It limits the number of requests required to get your inference done. It is advised to only
    /// set this flag to true after receiving a 503 error as it will limit hanging in your
    /// application to known places.
    /// </summary>
    public bool WaitForModel { get; set; }
}
