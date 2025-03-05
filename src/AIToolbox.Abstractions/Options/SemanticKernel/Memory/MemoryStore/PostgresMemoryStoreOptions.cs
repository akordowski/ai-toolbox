namespace AIToolbox.Options.SemanticKernel;

public sealed class PostgresMemoryStoreOptions
{
    public string ConnectionString { get; set; } = default!;
    public int VectorSize { get; set; }
    public string Schema { get; set; } = "public";
}
