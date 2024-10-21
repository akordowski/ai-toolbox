namespace AIToolbox.Options.KernelMemory;

public sealed class MemoryDbOptions
{
    public AzureAISearchOptions? AzureAISearch { get; set; }
    public ElasticSearchOptions? Elasticsearch { get; set; }
    public MongoDbAtlasOptions? MongoDbAtlas { get; set; }
    public PostgresOptions? Postgres { get; set; }
    public QdrantOptions? Qdrant { get; set; }
    public RedisOptions? Redis { get; set; }
    public SimpleTextDbOptions? SimpleTextDb { get; set; }
    public SimpleVectorDbOptions? SimpleVectorDb { get; set; }
    public SqlServerOptions? SqlServer { get; set; }
}
