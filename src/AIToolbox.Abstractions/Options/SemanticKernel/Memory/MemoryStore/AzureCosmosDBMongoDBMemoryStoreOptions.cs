namespace AIToolbox.Options.SemanticKernel;

public sealed class AzureCosmosDBMongoDBMemoryStoreOptions
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public int Dimensions { get; set; }
}
