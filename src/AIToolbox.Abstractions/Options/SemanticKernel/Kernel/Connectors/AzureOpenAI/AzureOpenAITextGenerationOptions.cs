namespace AIToolbox.Options.SemanticKernel;

public sealed class AzureOpenAITextGenerationOptions
{
    public string? Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public string DeploymentName { get; set; } = default!;
    public string? ServiceId { get; set; }
    public string? ModelId { get; set; }
}
