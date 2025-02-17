namespace AIToolbox.Options.Connectors;

public sealed class GlobalOpenAIOptions
{
    public string ApiKey { get; set; } = default!;
    public string? OrgId { get; set; }
    public string? ServiceId { get; set; }
}
