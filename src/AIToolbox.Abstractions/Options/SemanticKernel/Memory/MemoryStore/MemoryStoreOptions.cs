namespace AIToolbox.Options.SemanticKernel;

public sealed class MemoryStoreOptions
{
    public AzureAISearchMemoryStoreOptions? AzureAISearch { get; set; }
    public AzureCosmosDBMongoDBMemoryStoreOptions? AzureCosmosDBMongoDB { get; set; }
    public AzureCosmosDBNoSQLMemoryStoreOptions? AzureCosmosDBNoSQL { get; set; }
    public ChromaMemoryStoreOptions? Chroma { get; set; }
    public DuckDBMemoryStoreOptions? DuckDB { get; set; }
    public KustoMemoryStoreOptions? Kusto { get; set; }
    public MilvusMemoryStoreOptions? Milvus { get; set; }
    public MongoDBMemoryStoreOptions? MongoDB { get; set; }
    public PineconeMemoryStoreOptions? Pinecone { get; set; }
    public PostgresMemoryStoreOptions? Postgres { get; set; }
    public QdrantMemoryStoreOptions? Qdrant { get; set; }
    public RedisMemoryStoreOptions? Redis { get; set; }
    public SqliteMemoryStoreOptions? Sqlite { get; set; }
    public SqlServerMemoryStoreOptions? SqlServer { get; set; }
    public WeaviateMemoryStoreOptions? Weaviate { get; set; }
}
