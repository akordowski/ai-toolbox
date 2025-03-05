namespace AIToolbox.Options.SemanticKernel;

public sealed class SqlServerMemoryStoreOptions
{
    public string ConnectionString { get; set; } = default!;
    public string Schema { get; set; } = "dbo";
}
