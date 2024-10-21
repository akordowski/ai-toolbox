namespace AIToolbox.Options.KernelMemory;

public sealed class SqlServerOptions
{
    public string ConnectionString { get; set; } = default!;
    public string Schema { get; set; } = "dbo";
    public string MemoryCollectionTableName { get; set; } = "KMCollections";
    public string MemoryTableName { get; set; } = "KMMemories";
    public string EmbeddingsTableName { get; set; } = "KMEmbeddings";
    public string TagsTableName { get; set; } = "KMMemoriesTags";
}
