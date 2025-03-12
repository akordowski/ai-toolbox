using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.Enums;
using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.TestHelper;

public static class TestOptions
{
    private const string EndpointPrefix = "http://localhost/";

    public static AIToolboxOptions AIToolbox { get; } = new()
    {
        GlobalConnectors = new GlobalConnectorOptions
        {
            AzureOpenAI = new GlobalAzureOpenAIOptions
            {
                Endpoint = EndpointPrefix + "GlobalAzureOpenAIOptionsEndpoint",
                ApiKey = "GlobalAzureOpenAIOptionsApiKey",
                ServiceId = "GlobalAzureOpenAIOptionsServiceId"
            },
            Google = new GlobalGoogleOptions
            {
                ApiKey = "GlobalGoogleOptionsApiKey",
                ApiVersion = GoogleAIVersion.V1,
                ServiceId = "GlobalGoogleOptionsServiceId"
            },
            HuggingFace = new GlobalHuggingFaceOptions
            {
                Endpoint = EndpointPrefix + "GlobalHuggingFaceOptionsEndpoint",
                ApiKey = "GlobalHuggingFaceOptionsApiKey",
                ServiceId = "GlobalHuggingFaceOptionsServiceId"
            },
            MistralAI = new GlobalMistralAIOptions
            {
                Endpoint = EndpointPrefix + "GlobalMistralAIOptionsEndpoint",
                ApiKey = "GlobalMistralAIOptionsApiKey",
                ServiceId = "GlobalMistralAIOptionsServiceId"
            },
            Ollama = new GlobalOllamaOptions
            {
                Endpoint = EndpointPrefix + "GlobalOllamaOptionsEndpoint",
                ServiceId = "GlobalOllamaOptionsServiceId"
            },
            OpenAI = new GlobalOpenAIOptions
            {
                ApiKey = "GlobalOpenAIOptionsApiKey",
                OrgId = "GlobalOpenAIOptionsOrgId",
                ServiceId = "GlobalOpenAIOptionsServiceId"
            },
            VertexAI = new GlobalVertexAIOptions
            {
                BearerKey = "GlobalVertexAIOptionsBearerKey",
                Location = "GlobalVertexAIOptionsLocation",
                ProjectId = "GlobalVertexAIOptionsProjectId",
                ApiVersion = VertexAIVersion.V1,
                ServiceId = "GlobalVertexAIOptionsServiceId"
            }
        },
        SemanticKernel = new SemanticKernelOptions
        {
            Kernel = new KernelOptions
            {
                Connectors = new ConnectorOptions
                {
                    AzureOpenAI = new AzureOpenAIOptions
                    {
                        AudioToText = new AzureOpenAIAudioToTextOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAIAudioToTextOptionsEndpoint",
                            ApiKey = "AzureOpenAIAudioToTextOptionsApiKey",
                            DeploymentName = "AzureOpenAIAudioToTextOptionsDeploymentName",
                            ServiceId = "AzureOpenAIAudioToTextOptionsServiceId",
                            ModelId = "AzureOpenAIAudioToTextOptionsModelId"
                        },
                        ChatCompletion = new AzureOpenAIChatCompletionOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAIChatCompletionOptionsEndpoint",
                            ApiKey = "AzureOpenAIChatCompletionOptionsApiKey",
                            DeploymentName = "AzureOpenAIChatCompletionOptionsDeploymentName",
                            ServiceId = "AzureOpenAIChatCompletionOptionsServiceId",
                            ModelId = "AzureOpenAIChatCompletionOptionsModelId"
                        },
                        Files = new AzureOpenAIFilesOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAIFilesOptionsEndpoint",
                            ApiKey = "AzureOpenAIFilesOptionsApiKey",
                            OrgId = "AzureOpenAIFilesOptionsOrgId",
                            Version = "AzureOpenAIFilesOptionsVersion",
                            ServiceId = "AzureOpenAIFilesOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new AzureOpenAITextEmbeddingGenerationOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAITextEmbeddingGenerationOptionsEndpoint",
                            ApiKey = "AzureOpenAITextEmbeddingGenerationOptionsApiKey",
                            DeploymentName = "AzureOpenAITextEmbeddingGenerationOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextEmbeddingGenerationOptionsServiceId",
                            ModelId = "AzureOpenAITextEmbeddingGenerationOptionsModelId",
                            Dimensions = 1024
                        },
                        TextGeneration = new AzureOpenAITextGenerationOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAITextGenerationOptionsEndpoint",
                            ApiKey = "AzureOpenAITextGenerationOptionsApiKey",
                            DeploymentName = "AzureOpenAITextGenerationOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextGenerationOptionsServiceId",
                            ModelId = "AzureOpenAITextGenerationOptionsModelId"
                        },
                        TextToAudio = new AzureOpenAITextToAudioOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAITextToAudioOptionsEndpoint",
                            ApiKey = "AzureOpenAITextToAudioOptionsApiKey",
                            DeploymentName = "AzureOpenAITextToAudioOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextToAudioOptionsServiceId",
                            ModelId = "AzureOpenAITextToAudioOptionsModelId"
                        },
                        TextToImage = new AzureOpenAITextToImageOptions
                        {
                            Endpoint = EndpointPrefix + "AzureOpenAITextToImageOptionsEndpoint",
                            ApiKey = "AzureOpenAITextToImageOptionsApiKey",
                            DeploymentName = "AzureOpenAITextToImageOptionsDeploymentName",
                            ServiceId = "AzureOpenAITextToImageOptionsServiceId",
                            ModelId = "AzureOpenAITextToImageOptionsModelId"
                        }
                    },
                    Google = new GoogleOptions
                    {
                        ChatCompletion = new GoogleAIChatCompletionOptions
                        {
                            ModelId = "GoogleAIChatCompletionOptionsModelId",
                            ApiKey = "GoogleAIChatCompletionOptionsApiKey",
                            ApiVersion = GoogleAIVersion.V1,
                            ServiceId = "GoogleAIChatCompletionOptionsServiceId"
                        },
                        EmbeddingGeneration = new GoogleAIEmbeddingGenerationOptions
                        {
                            ModelId = "GoogleAIEmbeddingGenerationOptionsModelId",
                            ApiKey = "GoogleAIEmbeddingGenerationOptionsApiKey",
                            ApiVersion = GoogleAIVersion.V1,
                            ServiceId = "GoogleAIEmbeddingGenerationOptionsServiceId"
                        }
                    },
                    HuggingFace = new HuggingFaceOptions
                    {
                        ChatCompletion = new HuggingFaceChatCompletionOptions
                        {
                            Model = "HuggingFaceChatCompletionOptionsModel",
                            Endpoint = EndpointPrefix + "HuggingFaceChatCompletionOptionsEndpoint",
                            ApiKey = "HuggingFaceChatCompletionOptionsApiKey",
                            ServiceId = "HuggingFaceChatCompletionOptionsServiceId"
                        },
                        ImageToText = new HuggingFaceImageToTextOptions
                        {
                            Model = "HuggingFaceImageToTextOptionsModel",
                            Endpoint = EndpointPrefix + "HuggingFaceImageToTextOptionsEndpoint",
                            ApiKey = "HuggingFaceImageToTextOptionsApiKey",
                            ServiceId = "HuggingFaceImageToTextOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new HuggingFaceTextEmbeddingGenerationOptions
                        {
                            Model = "HuggingFaceTextEmbeddingGenerationOptionsModel",
                            Endpoint = EndpointPrefix + "HuggingFaceTextEmbeddingGenerationOptionsEndpoint",
                            ApiKey = "HuggingFaceTextEmbeddingGenerationOptionsApiKey",
                            ServiceId = "HuggingFaceTextEmbeddingGenerationOptionsServiceId"
                        },
                        TextGeneration = new HuggingFaceTextGenerationOptions
                        {
                            Model = "HuggingFaceTextGenerationOptionsModel",
                            Endpoint = EndpointPrefix + "HuggingFaceTextGenerationOptionsEndpoint",
                            ApiKey = "HuggingFaceTextGenerationOptionsApiKey",
                            ServiceId = "HuggingFaceTextGenerationOptionsServiceId"
                        }
                    },
                    MistralAI = new MistralAIOptions
                    {
                        ChatCompletion = new MistralAIChatCompletionOptions
                        {
                            Model = "MistralAIChatCompletionOptionsModel",
                            Endpoint = EndpointPrefix + "MistralAIChatCompletionOptionsEndpoint",
                            ApiKey = "MistralAIChatCompletionOptionsApiKey",
                            ServiceId = "MistralAIChatCompletionOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new MistralAITextEmbeddingGenerationOptions
                        {
                            Model = "MistralAITextEmbeddingGenerationOptionsModel",
                            Endpoint = EndpointPrefix + "MistralAITextEmbeddingGenerationOptionsEndpoint",
                            ApiKey = "MistralAITextEmbeddingGenerationOptionsApiKey",
                            ServiceId = "MistralAITextEmbeddingGenerationOptionsServiceId"
                        }
                    },
                    Ollama = new OllamaOptions
                    {
                        ChatCompletion = new OllamaChatCompletionOptions
                        {
                            ModelId = "OllamaChatCompletionOptionsModelId",
                            Endpoint = EndpointPrefix + "OllamaChatCompletionOptionsEndpoint",
                            ServiceId = "OllamaChatCompletionOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new OllamaTextEmbeddingGenerationOptions
                        {
                            ModelId = "OllamaTextEmbeddingGenerationOptionsModelId",
                            Endpoint = EndpointPrefix + "OllamaTextEmbeddingGenerationOptionsEndpoint",
                            ServiceId = "OllamaTextEmbeddingGenerationOptionsServiceId"
                        },
                        TextGeneration = new OllamaTextGenerationOptions
                        {
                            ModelId = "OllamaTextGenerationOptionsModelId",
                            Endpoint = EndpointPrefix + "OllamaTextGenerationOptionsEndpoint",
                            ServiceId = "OllamaTextGenerationOptionsServiceId"
                        }
                    },
                    OpenAI = new OpenAIOptions
                    {
                        AudioToText = new OpenAIAudioToTextOptions
                        {
                            ModelId = "OpenAIAudioToTextOptionsModelId",
                            ApiKey = "OpenAIAudioToTextOptionsApiKey",
                            OrgId = "OpenAIAudioToTextOptionsOrgId",
                            ServiceId = "OpenAIAudioToTextOptionsServiceId"
                        },
                        ChatCompletion = new OpenAIChatCompletionOptions
                        {
                            ModelId = "OpenAIChatCompletionOptionsModelId",
                            ApiKey = "OpenAIChatCompletionOptionsApiKey",
                            OrgId = "OpenAIChatCompletionOptionsOrgId",
                            ServiceId = "OpenAIChatCompletionOptionsServiceId"
                        },
                        Files = new OpenAIFilesOptions
                        {
                            ApiKey = "OpenAIFilesOptionsApiKey",
                            OrgId = "OpenAIFilesOptionsOrgId",
                            ServiceId = "OpenAIFilesOptionsServiceId"
                        },
                        TextEmbeddingGeneration = new OpenAITextEmbeddingGenerationOptions
                        {
                            ModelId = "OpenAITextEmbeddingGenerationOptionsModelId",
                            ApiKey = "OpenAITextEmbeddingGenerationOptionsApiKey",
                            OrgId = "OpenAITextEmbeddingGenerationOptionsOrgId",
                            ServiceId = "OpenAITextEmbeddingGenerationOptionsServiceId",
                            Dimensions = 1024
                        },
                        TextToAudio = new OpenAITextToAudioOptions
                        {
                            ModelId = "OpenAITextToAudioOptionsModelId",
                            ApiKey = "OpenAITextToAudioOptionsApiKey",
                            OrgId = "OpenAITextToAudioOptionsOrgId",
                            ServiceId = "OpenAITextToAudioOptionsServiceId"
                        },
                        TextToImage = new OpenAITextToImageOptions
                        {
                            ApiKey = "OpenAITextToImageOptionsApiKey",
                            OrgId = "OpenAITextToImageOptionsOrgId",
                            ServiceId = "OpenAITextToImageOptionsServiceId"
                        }
                    },
                    VertexAI = new VertexAIOptions
                    {
                        ChatCompletion = new VertexAIChatCompletionOptions
                        {
                            ModelId = "VertexAIChatCompletionOptionsModelId",
                            BearerKey = "VertexAIChatCompletionOptionsBearerKey",
                            Location = "VertexAIChatCompletionOptionsLocation",
                            ProjectId = "VertexAIChatCompletionOptionsProjectId",
                            ApiVersion = VertexAIVersion.V1,
                            ServiceId = "VertexAIChatCompletionOptionsServiceId"
                        },
                        EmbeddingGeneration = new VertexAIEmbeddingGenerationOptions
                        {
                            ModelId = "VertexAIEmbeddingGenerationOptionsModelId",
                            BearerKey = "VertexAIEmbeddingGenerationOptionsBearerKey",
                            Location = "VertexAIEmbeddingGenerationOptionsLocation",
                            ProjectId = "VertexAIEmbeddingGenerationOptionsProjectId",
                            ApiVersion = VertexAIVersion.V1,
                            ServiceId = "VertexAIEmbeddingGenerationOptionsServiceId"
                        }
                    }
                }
            },
            Memory = new MemoryOptions
            {
                Store = new MemoryStoreOptions
                {
                    AzureAISearch = new AzureAISearchMemoryStoreOptions
                    {
                        Endpoint = EndpointPrefix + "AzureAISearchMemoryStoreOptionsEndpoint",
                        ApiKey = "AzureAISearchMemoryStoreOptionsApiKey"
                    },
                    AzureCosmosDBMongoDB = new AzureCosmosDBMongoDBMemoryStoreOptions
                    {
                        ConnectionString = "AzureCosmosDBMongoDBMemoryStoreOptionsConnectionString",
                        DatabaseName = "AzureCosmosDBMongoDBMemoryStoreOptionsDatabaseName",
                        Dimensions = 1024
                    },
                    AzureCosmosDBNoSQL = new AzureCosmosDBNoSQLMemoryStoreOptions
                    {
                        ConnectionString = "AzureCosmosDBNoSQLMemoryStoreOptionsConnectionString",
                        DatabaseName = "AzureCosmosDBNoSQLMemoryStoreOptionsDatabaseName",
                        Dimensions = 1024,
                        VectorDataType = VectorDataType.Int8,
                        VectorIndexType = VectorIndexType.QuantizedFlat,
                        ApplicationName = "AzureCosmosDBNoSQLMemoryStoreOptionsApplicationName"
                    },
                    Chroma = new ChromaMemoryStoreOptions
                    {
                        Endpoint = EndpointPrefix + "ChromaMemoryStoreOptionsEndpoint"
                    },
                    DuckDB = new DuckDBMemoryStoreOptions
                    {
                        Filename = "DuckDBMemoryStoreOptionsFilename",
                        VectorSize = 1024
                    },
                    Kusto = new KustoMemoryStoreOptions
                    {
                        Database = "KustoMemoryStoreOptionsDatabase"
                    },
                    Milvus = new MilvusMemoryStoreOptions
                    {
                        Host = "MilvusMemoryStoreOptionsHost",
                        Port = 1024,
                        Ssl = true,
                        Database = "MilvusMemoryStoreOptionsDatabase",
                        IndexName = "MilvusMemoryStoreOptionsIndexName",
                        VectorSize = 1024,
                        MetricType = SimilarityMetricType.Substructure,
                        ConsistencyLevel = ConsistencyLevel.Customized
                    },
                    MongoDB = new MongoDBMemoryStoreOptions
                    {
                        ConnectionString = "MongoDBMemoryStoreOptionsConnectionString",
                        DatabaseName = "MongoDBMemoryStoreOptionsDatabaseName",
                        IndexName = "MongoDBMemoryStoreOptionsIndexName"
                    },
                    Pinecone = new PineconeMemoryStoreOptions
                    {
                        PineconeEnvironment = "PineconeMemoryStoreOptionsPineconeEnvironment",
                        ApiKey = "PineconeMemoryStoreOptionsApiKey"
                    },
                    Postgres = new PostgresMemoryStoreOptions
                    {
                        ConnectionString = "PostgresMemoryStoreOptionsConnectionString",
                        VectorSize = 1024,
                        Schema = "PostgresMemoryStoreOptionsSchema"
                    },
                    Qdrant = new QdrantMemoryStoreOptions
                    {
                        Endpoint = EndpointPrefix + "QdrantMemoryStoreOptionsEndpoint",
                        VectorSize = 1024
                    },
                    Redis = new RedisMemoryStoreOptions
                    {
                        ConnectionString = "RedisMemoryStoreOptionsConnectionString",
                        VectorSize = 1024,
                        VectorIndexAlgorithm = VectorIndexAlgorithm.HNSW,
                        VectorDistanceMetric = VectorDistanceMetric.COSINE,
                        QueryDialect = 1
                    },
                    Sqlite = new SqliteMemoryStoreOptions
                    {
                        Filename = "SqliteMemoryStoreOptionsFilename"
                    },
                    SqlServer = new SqlServerMemoryStoreOptions
                    {
                        ConnectionString = "SqlServerMemoryStoreOptionsConnectionString",
                        Schema = "SqlServerMemoryStoreOptionsSchema"
                    },
                    Weaviate = new WeaviateMemoryStoreOptions
                    {
                        Endpoint = EndpointPrefix + "WeaviateMemoryStoreOptionsEndpoint",
                        ApiKey = "WeaviateMemoryStoreOptionsApiKey",
                        ApiVersion = "WeaviateMemoryStoreOptionsApiVersion"
                    }
                }
            }
        }
    };
}
