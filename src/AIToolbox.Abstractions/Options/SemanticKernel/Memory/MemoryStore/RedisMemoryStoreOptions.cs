using AIToolbox.Options.Enums;

namespace AIToolbox.Options.SemanticKernel;

public sealed class RedisMemoryStoreOptions
{
    public string ConnectionString { get; set; } = default!;
    public int VectorSize { get; set; } = 1536;
    public VectorIndexAlgorithm VectorIndexAlgorithm { get; set; } = VectorIndexAlgorithm.HNSW;
    public VectorDistanceMetric VectorDistanceMetric { get; set; } = VectorDistanceMetric.COSINE;
    public int QueryDialect { get; set; } = 2;
}
