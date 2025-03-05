namespace AIToolbox.Options.SemanticKernel;

public sealed class MongoDBMemoryStoreOptions
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string? IndexName { get; set; }
}
