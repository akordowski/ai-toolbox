using AIToolbox.Options.SemanticKernel;
using AIToolbox.TestHelper;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTestData
{
    public static TheoryData<Action<IAIToolboxBuilder, SemanticKernelOptions>> ConfigureByOptionsAction =>
    [
        (aiToolbox, opt) =>
        {
            aiToolbox.AddSemanticKernel(
                semanticKernel => semanticKernel
                    .AddKernel(TestBuilder.AddKernelMethods)
                    .AddMemory(TestBuilder.AddMemoryMethods),
                    options =>
                    {
                        options.Kernel = opt.Kernel;
                        options.Memory = opt.Memory;
                    });
        },
        (aiToolbox, opt) =>
        {
            aiToolbox.AddSemanticKernel(semanticKernel =>
                semanticKernel
                    .AddKernel(
                        TestBuilder.AddKernelMethods,
                        options => options.Connectors = opt.Kernel!.Connectors)
                    .AddMemory(
                        TestBuilder.AddMemoryMethods,
                        options => options.Store = opt.Memory!.Store));
        },
        (aiToolbox, opt) =>
        {
            aiToolbox.AddSemanticKernel(
                semanticKernel =>
                {
                    semanticKernel
                        .AddKernel(kernel =>
                        {
                            var connectorOptions = opt.Kernel!.Connectors!;
                            var azureOpenAI = connectorOptions.AzureOpenAI!;
                            var google = connectorOptions.Google!;
                            var huggingFace = connectorOptions.HuggingFace!;
                            var mistralAI = connectorOptions.MistralAI!;
                            var ollama = connectorOptions.Ollama!;
                            var openAI = connectorOptions.OpenAI!;
                            var vertexAI = connectorOptions.VertexAI!;

                            kernel
                                .WithAzureOpenAIConnector(options =>
                                {
                                    options.AudioToText = azureOpenAI.AudioToText;
                                    options.ChatCompletion = azureOpenAI.ChatCompletion;
                                    options.Files = azureOpenAI.Files;
                                    options.TextEmbeddingGeneration = azureOpenAI.TextEmbeddingGeneration;
                                    options.TextGeneration = azureOpenAI.TextGeneration;
                                    options.TextToAudio = azureOpenAI.TextToAudio;
                                    options.TextToImage = azureOpenAI.TextToImage;
                                })
                                .WithGoogleConnector(options =>
                                {
                                    options.ChatCompletion = google.ChatCompletion;
                                    options.EmbeddingGeneration = google.EmbeddingGeneration;
                                })
                                .WithHuggingFaceConnector(options =>
                                {
                                    options.ChatCompletion = huggingFace.ChatCompletion;
                                    options.ImageToText = huggingFace.ImageToText;
                                    options.TextEmbeddingGeneration = huggingFace.TextEmbeddingGeneration;
                                    options.TextGeneration = huggingFace.TextGeneration;
                                })
                                .WithMistralAIConnector(options =>
                                {
                                    options.ChatCompletion = mistralAI.ChatCompletion;
                                    options.TextEmbeddingGeneration = mistralAI.TextEmbeddingGeneration;
                                })
                                .WithOllamaConnector(options =>
                                {
                                    options.ChatCompletion = ollama.ChatCompletion;
                                    options.TextEmbeddingGeneration = ollama.TextEmbeddingGeneration;
                                    options.TextGeneration = ollama.TextGeneration;
                                })
                                .WithOpenAIConnector(options =>
                                {
                                    options.AudioToText = openAI.AudioToText;
                                    options.ChatCompletion = openAI.ChatCompletion;
                                    options.Files = openAI.Files;
                                    options.TextEmbeddingGeneration = openAI.TextEmbeddingGeneration;
                                    options.TextToAudio = openAI.TextToAudio;
                                    options.TextToImage = openAI.TextToImage;
                                })
                                .WithVertexAIConnector(options =>
                                {
                                    options.ChatCompletion = vertexAI.ChatCompletion;
                                    options.EmbeddingGeneration = vertexAI.EmbeddingGeneration;
                                });
                        })
                        .AddMemory(memory =>
                        {
                            var storeOptions = opt.Memory!.Store!;
                            var azureAISearch = storeOptions.AzureAISearch!;
                            var azureCosmosDBMongoDB = storeOptions.AzureCosmosDBMongoDB!;
                            var azureCosmosDBNoSQL = storeOptions.AzureCosmosDBNoSQL!;
                            var chroma = storeOptions.Chroma!;
                            var duckDB = storeOptions.DuckDB!;
                            var kusto = storeOptions.Kusto!;
                            var milvus = storeOptions.Milvus!;
                            var mongoDB = storeOptions.MongoDB!;
                            var pinecone = storeOptions.Pinecone!;
                            var postgres = storeOptions.Postgres!;
                            var qdrant = storeOptions.Qdrant!;
                            var redis = storeOptions.Redis!;
                            var sqlite = storeOptions.Sqlite!;
                            var sqlServer = storeOptions.SqlServer!;
                            var weaviate = storeOptions.Weaviate!;

                            memory
                                .WithAzureAISearchMemoryStore(options =>
                                {
                                    options.Endpoint = azureAISearch.Endpoint;
                                    options.ApiKey = azureAISearch.ApiKey;
                                })
                                .WithAzureCosmosDBMongoDBMemoryStore(options =>
                                {
                                    options.ConnectionString = azureCosmosDBMongoDB.ConnectionString;
                                    options.DatabaseName = azureCosmosDBMongoDB.DatabaseName;
                                    options.Dimensions = azureCosmosDBMongoDB.Dimensions;
                                })
                                .WithAzureCosmosDBNoSQLMemoryStore(options =>
                                {
                                    options.ConnectionString = azureCosmosDBNoSQL.ConnectionString;
                                    options.DatabaseName = azureCosmosDBNoSQL.DatabaseName;
                                    options.Dimensions = azureCosmosDBNoSQL.Dimensions;
                                    options.VectorDataType = azureCosmosDBNoSQL.VectorDataType;
                                    options.VectorIndexType = azureCosmosDBNoSQL.VectorIndexType;
                                    options.ApplicationName = azureCosmosDBNoSQL.ApplicationName;
                                })
                                .WithChromaMemoryStore(options =>
                                {
                                    options.Endpoint = chroma.Endpoint;
                                })
                                .WithDuckDBMemoryStore(options =>
                                {
                                    options.Filename = duckDB.Filename;
                                    options.VectorSize = duckDB.VectorSize;
                                })
                                .WithKustoMemoryStore(options =>
                                {
                                    options.Database = kusto.Database;
                                })
                                .WithMilvusMemoryStore(options =>
                                {
                                    options.Host = milvus.Host;
                                    options.Port = milvus.Port;
                                    options.Ssl = milvus.Ssl;
                                    options.Database = milvus.Database;
                                    options.IndexName = milvus.IndexName;
                                    options.VectorSize = milvus.VectorSize;
                                    options.MetricType = milvus.MetricType;
                                    options.ConsistencyLevel = milvus.ConsistencyLevel;
                                })
                                .WithMongoDBMemoryStore(options =>
                                {
                                    options.ConnectionString = mongoDB.ConnectionString;
                                    options.DatabaseName = mongoDB.DatabaseName;
                                    options.IndexName = mongoDB.IndexName;
                                })
                                .WithPineconeMemoryStore(options =>
                                {
                                    options.PineconeEnvironment = pinecone.PineconeEnvironment;
                                    options.ApiKey = pinecone.ApiKey;
                                })
                                .WithPostgresMemoryStore(options =>
                                {
                                    options.ConnectionString = postgres.ConnectionString;
                                    options.VectorSize = postgres.VectorSize;
                                    options.Schema = postgres.Schema;
                                })
                                .WithQdrantMemoryStore(options =>
                                {
                                    options.Endpoint = qdrant.Endpoint;
                                    options.VectorSize = qdrant.VectorSize;
                                })
                                .WithRedisMemoryStore(options =>
                                {
                                    options.ConnectionString = redis.ConnectionString;
                                    options.VectorSize = redis.VectorSize;
                                    options.VectorIndexAlgorithm = redis.VectorIndexAlgorithm;
                                    options.VectorDistanceMetric = redis.VectorDistanceMetric;
                                    options.QueryDialect = redis.QueryDialect;
                                })
                                .WithSqliteMemoryStore(options =>
                                {
                                    options.Filename = sqlite.Filename;
                                })
                                .WithSqlServerMemoryStore(options =>
                                {
                                    options.ConnectionString = sqlServer.ConnectionString;
                                    options.Schema = sqlServer.Schema;
                                })
                                .WithWeaviateMemoryStore(options =>
                                {
                                    options.Endpoint = weaviate.Endpoint;
                                    options.ApiKey = weaviate.ApiKey;
                                    options.ApiVersion = weaviate.ApiVersion;
                                });
                        });
                });
        }
    ];
}
