namespace AIToolbox.Options.SemanticKernel;

public sealed class OpenAIChatCompletionOptions
{
    public string ModelId { get; set; } = default!;
    public string? ApiKey { get; set; }
    public string? OrgId { get; set; }
    public string? ServiceId { get; set; }
}
