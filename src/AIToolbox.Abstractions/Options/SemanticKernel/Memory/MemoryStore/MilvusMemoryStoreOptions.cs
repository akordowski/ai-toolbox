namespace AIToolbox.Options.SemanticKernel;

public sealed class MilvusMemoryStoreOptions
{
    public string Host { get; set; } = default!;
    public int Port { get; set; } = 19530;
    public bool Ssl { get; set; }
    public string? Database { get; set; }
    public string? IndexName { get; set; }
    public int VectorSize { get; set; } = 1536;
    public SimilarityMetricType MetricType { get; set; } = SimilarityMetricType.Ip;
    public ConsistencyLevel ConsistencyLevel { get; set; } = ConsistencyLevel.Session;
}

public enum ConsistencyLevel
{
    Strong = 0,
    Session = 1,
    BoundedStaleness = 2,
    Eventually = 3,
    Customized = 4
}

public enum SimilarityMetricType
{
    Invalid,
    L2,
    Ip,
    Cosine,
    Jaccard,
    Tanimoto,
    Hamming,
    Superstructure,
    Substructure
}
