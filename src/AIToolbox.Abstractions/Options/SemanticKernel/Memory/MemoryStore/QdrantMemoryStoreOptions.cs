namespace AIToolbox.Options.SemanticKernel;

public sealed class QdrantMemoryStoreOptions
{
    public string Endpoint { get; set; } = default!;
    public int VectorSize { get; set; }
}
