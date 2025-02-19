namespace AIToolbox.Options.SemanticKernel;

public sealed class HuggingFaceChatCompletionOptions
{
    public string Model { get; set; } = default!;
    public string? Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public string? ServiceId { get; set; }
}
