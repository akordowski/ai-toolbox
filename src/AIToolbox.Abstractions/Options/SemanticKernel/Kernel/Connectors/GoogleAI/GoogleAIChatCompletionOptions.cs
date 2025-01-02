namespace AIToolbox.Options.SemanticKernel;

public sealed class GoogleAIChatCompletionOptions
{
    public string ModelId { get; set; } = default!;
    public string? ApiKey { get; set; } = default!;
    public GoogleAIVersion? ApiVersion { get; set; } = default!;
    public string? ServiceId { get; set; }
}
