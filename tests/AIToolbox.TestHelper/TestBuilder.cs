using AIToolbox.DependencyInjection;

namespace AIToolbox.TestHelper;

public static class TestBuilder
{
    public static void AddGlobalConnectorMethods(IGlobalConnectorBuilder builder) =>
        builder
            .AddGlobalAzureOpenAIOptions()
            .AddGlobalGoogleOptions()
            .AddGlobalHuggingFaceOptions()
            .AddGlobalMistralAIOptions()
            .AddGlobalOllamaOptions()
            .AddGlobalOpenAIOptions()
            .AddGlobalVertexAIOptions();

    public static void AddKernelMethods(IKernelBuilder builder) =>
        builder
            .WithAzureOpenAIConnector()
            .WithGoogleConnector()
            .WithHuggingFaceConnector()
            .WithMistralAIConnector()
            .WithOllamaConnector()
            .WithOpenAIConnector()
            .WithVertexAIConnector();

    public static void AddMemoryMethods(IMemoryBuilder builder) =>
        builder
            .WithAzureAISearchMemoryStore()
            .WithAzureCosmosDBMongoDBMemoryStore()
            .WithAzureCosmosDBNoSQLMemoryStore()
            .WithChromaMemoryStore()
            .WithDuckDBMemoryStore()
            .WithKustoMemoryStore()
            .WithMilvusMemoryStore()
            .WithMongoDBMemoryStore()
            .WithPineconeMemoryStore()
            .WithPostgresMemoryStore()
            .WithQdrantMemoryStore()
            .WithRedisMemoryStore()
            .WithSqliteMemoryStore()
            .WithSqlServerMemoryStore()
            .WithWeaviateMemoryStore();
}
