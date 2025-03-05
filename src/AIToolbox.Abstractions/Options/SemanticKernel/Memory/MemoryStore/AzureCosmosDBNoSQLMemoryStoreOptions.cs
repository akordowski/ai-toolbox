namespace AIToolbox.Options.SemanticKernel;

public sealed class AzureCosmosDBNoSQLMemoryStoreOptions
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public ulong Dimensions { get; set; }
    public VectorDataType VectorDataType { get; set; }
    public VectorIndexType VectorIndexType { get; set; }
    public string? ApplicationName { get; set; }
}

#pragma warning disable CA1720 // Identifiers should not contain type names
public enum VectorDataType
{
    Float16,
    Float32,
    Uint8,
    Int8
}
#pragma warning restore CA1720 // Identifiers should not contain type names

public enum VectorIndexType
{
    Flat,
    DiskANN,
    QuantizedFlat
}
