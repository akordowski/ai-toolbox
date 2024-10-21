namespace AIToolbox.Options.KernelMemory;

public sealed class CustomEmbeddingGeneratorOptions
{
    public bool UseForIngestion { get; set; } = true;
    public bool UseForRetrieval { get; set; } = true;
}
