namespace AIToolbox.Options.KernelMemory;

public sealed class RedisOptions
{
    public string ConnectionString { get; set; } = default!;
    public VectorIndexAlgorithm VectorAlgorithm { get; set; } = VectorIndexAlgorithm.HNSW;
}
