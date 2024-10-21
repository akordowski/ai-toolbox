namespace AIToolbox.Options.KernelMemory;

public sealed class KernelMemoryOptions
{
    public DataIngestionOptions? DataIngestion { get; set; }
    public DocumentStorageOptions? DocumentStorage { get; set; }
    public MemoryDbOptions? Memory { get; set; }
    public TextEmbeddingGenerationOptions? TextEmbeddingGeneration { get; set; }
    public TextGenerationOptions? TextGeneration { get; set; }
}
