namespace AIToolbox.Options.KernelMemory;

public class AzureOpenAIOptions
{
    public string? Endpoint { get; set; } = default!;
    public string? ApiKey { get; set; } = default!;
    public string Deployment { get; set; } = default!;
    public int MaxTokenTotal { get; set; } = 8191;
    public int? EmbeddingDimensions { get; set; }
    public int MaxEmbeddingBatchSize { get; set; } = 1;
    public int MaxRetries { get; set; } = 10;
}
