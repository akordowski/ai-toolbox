namespace AIToolbox.Options.KernelMemory;

public class OpenAIOptions
{
    public TextGenerationType TextGenerationType { get; set; }
    public string Endpoint { get; set; } = "https://api.openai.com/v1";
    public string? ApiKey { get; set; } = default!;
    public string? OrgId { get; set; } = default!;
    public string TextModel { get; set; } = default!;
    public int TextModelMaxTokenTotal { get; set; } = 8192;
    public string EmbeddingModel { get; set; } = default!;
    public int EmbeddingModelMaxTokenTotal { get; set; } = 8191;
    public int? EmbeddingDimensions { get; set; }
    public int MaxEmbeddingBatchSize { get; set; } = 100;
    public int MaxRetries { get; set; } = 10;
}
