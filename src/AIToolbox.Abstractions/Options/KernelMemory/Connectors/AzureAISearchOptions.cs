namespace AIToolbox.Options.KernelMemory;

public sealed class AzureAISearchOptions
{
    public string Endpoint { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public bool UseHybridSearch { get; set; }
}
