using AIToolbox.Options.Enums;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class MemoryBuilderExtensionsTestData
{
    public static TheoryData<Action> AddMemoryWithNullBuilder
    {
        get
        {
            IMemoryBuilder builder = null!;

            return
            [
                () => builder.WithAzureAISearchMemoryStore(),
                () => builder.WithAzureAISearchMemoryStore(_ => { }),

                () => builder.WithAzureCosmosDBMongoDBMemoryStore(),
                () => builder.WithAzureCosmosDBMongoDBMemoryStore(_ => { }),

                () => builder.WithAzureCosmosDBNoSQLMemoryStore(),
                () => builder.WithAzureCosmosDBNoSQLMemoryStore(_ => { }),

                () => builder.WithChromaMemoryStore(),
                () => builder.WithChromaMemoryStore(_ => { }),

                () => builder.WithDuckDBMemoryStore(),
                () => builder.WithDuckDBMemoryStore(_ => { }),

                () => builder.WithKustoMemoryStore(),
                () => builder.WithKustoMemoryStore(_ => { }),

                () => builder.WithMilvusMemoryStore(),
                () => builder.WithMilvusMemoryStore(_ => { }),

                () => builder.WithMongoDBMemoryStore(),
                () => builder.WithMongoDBMemoryStore(_ => { }),

                () => builder.WithPineconeMemoryStore(),
                () => builder.WithPineconeMemoryStore(_ => { }),

                () => builder.WithPostgresMemoryStore(),
                () => builder.WithPostgresMemoryStore(_ => { }),

                () => builder.WithQdrantMemoryStore(),
                () => builder.WithQdrantMemoryStore(_ => { }),

                () => builder.WithRedisMemoryStore(),
                () => builder.WithRedisMemoryStore(_ => { }),

                () => builder.WithSqliteMemoryStore(),
                () => builder.WithSqliteMemoryStore(_ => { }),

                () => builder.WithSqlServerMemoryStore(),
                () => builder.WithSqlServerMemoryStore(_ => { }),

                () => builder.WithWeaviateMemoryStore(),
                () => builder.WithWeaviateMemoryStore(_ => { })
            ];
        }
    }

    public static TheoryData<Action, string> AddMemoryWithNullParameters
    {
        get
        {
            var builder = new MemoryBuilder(new MemoryOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithAzureAISearchMemoryStore(null!), "optionsAction" },
                { () => builder.WithAzureCosmosDBMongoDBMemoryStore(null!), "optionsAction" },
                { () => builder.WithAzureCosmosDBNoSQLMemoryStore(null!), "optionsAction" },
                { () => builder.WithChromaMemoryStore(null!), "optionsAction" },
                { () => builder.WithDuckDBMemoryStore(null!), "optionsAction" },
                { () => builder.WithKustoMemoryStore(null!), "optionsAction" },
                { () => builder.WithMilvusMemoryStore(null!), "optionsAction" },
                { () => builder.WithMongoDBMemoryStore(null!), "optionsAction" },
                { () => builder.WithPineconeMemoryStore(null!), "optionsAction" },
                { () => builder.WithPostgresMemoryStore(null!), "optionsAction" },
                { () => builder.WithQdrantMemoryStore(null!), "optionsAction" },
                { () => builder.WithRedisMemoryStore(null!), "optionsAction" },
                { () => builder.WithSqliteMemoryStore(null!), "optionsAction" },
                { () => builder.WithSqlServerMemoryStore(null!), "optionsAction" },
                { () => builder.WithWeaviateMemoryStore(null!), "optionsAction" }
            };
        }
    }

    public static TheoryData<Action, string> AddMemoryWithNoDefaultOptions
    {
        get
        {
            var builder = new MemoryBuilder(new MemoryOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithAzureAISearchMemoryStore(), "No 'AzureAISearchMemoryStoreOptions' provided.*" },
                { () => builder.WithAzureCosmosDBMongoDBMemoryStore(), "No 'AzureCosmosDBMongoDBMemoryStoreOptions' provided.*" },
                { () => builder.WithAzureCosmosDBNoSQLMemoryStore(), "No 'AzureCosmosDBNoSQLMemoryStoreOptions' provided.*" },
                { () => builder.WithChromaMemoryStore(), "No 'ChromaMemoryStoreOptions' provided.*" },
                { () => builder.WithDuckDBMemoryStore(), "No 'DuckDBMemoryStoreOptions' provided.*" },
                { () => builder.WithKustoMemoryStore(), "No 'KustoMemoryStoreOptions' provided.*" },
                { () => builder.WithMilvusMemoryStore(), "No 'MilvusMemoryStoreOptions' provided.*" },
                { () => builder.WithMongoDBMemoryStore(), "No 'MongoDBMemoryStoreOptions' provided.*" },
                { () => builder.WithPineconeMemoryStore(), "No 'PineconeMemoryStoreOptions' provided.*" },
                { () => builder.WithPostgresMemoryStore(), "No 'PostgresMemoryStoreOptions' provided.*" },
                { () => builder.WithQdrantMemoryStore(), "No 'QdrantMemoryStoreOptions' provided.*" },
                { () => builder.WithRedisMemoryStore(), "No 'RedisMemoryStoreOptions' provided.*" },
                { () => builder.WithSqliteMemoryStore(), "No 'SqliteMemoryStoreOptions' provided.*" },
                { () => builder.WithSqlServerMemoryStore(), "No 'SqlServerMemoryStoreOptions' provided.*" },
                { () => builder.WithWeaviateMemoryStore(), "No 'WeaviateMemoryStoreOptions' provided.*" }
            };
        }
    }

    public static TheoryData<Func<IMemoryBuilder>, IMemoryBuilder, Action> AddMemoryWithDefaultOptions
    {
        get
        {
            var options = new MemoryOptions
            {
                Store = new MemoryStoreOptions
                {
                    AzureAISearch = new AzureAISearchMemoryStoreOptions(),
                    AzureCosmosDBMongoDB = new AzureCosmosDBMongoDBMemoryStoreOptions(),
                    AzureCosmosDBNoSQL = new AzureCosmosDBNoSQLMemoryStoreOptions(),
                    Chroma = new ChromaMemoryStoreOptions(),
                    DuckDB = new DuckDBMemoryStoreOptions(),
                    Kusto = new KustoMemoryStoreOptions(),
                    Milvus = new MilvusMemoryStoreOptions(),
                    MongoDB = new MongoDBMemoryStoreOptions(),
                    Pinecone = new PineconeMemoryStoreOptions(),
                    Postgres = new PostgresMemoryStoreOptions(),
                    Qdrant = new QdrantMemoryStoreOptions(),
                    Redis = new RedisMemoryStoreOptions(),
                    Sqlite = new SqliteMemoryStoreOptions(),
                    SqlServer = new SqlServerMemoryStoreOptions(),
                    Weaviate = new WeaviateMemoryStoreOptions()
                }
            };
            var services = new ServiceCollection();
            var builder = new MemoryBuilder(options, services);

            return new TheoryData<Func<IMemoryBuilder>, IMemoryBuilder, Action>
            {
                {
                    builder.WithAzureAISearchMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureAISearch.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureAISearchMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.AzureAISearch);

                        AssertAzureAISearchServices(services);
                    }
                },
                {
                    builder.WithAzureCosmosDBMongoDBMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureCosmosDBMongoDB.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureCosmosDBMongoDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.AzureCosmosDBMongoDB);

                        AssertAzureCosmosDBMongoDBServices(services);
                    }
                },
                {
                    builder.WithAzureCosmosDBNoSQLMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureCosmosDBNoSQL.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureCosmosDBNoSQLMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.AzureCosmosDBNoSQL);

                        AssertAzureCosmosDBNoSQLServices(services);
                    }
                },
                {
                    builder.WithChromaMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Chroma.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(ChromaMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Chroma);

                        AssertChromaServices(services);
                    }
                },
                {
                    builder.WithDuckDBMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.DuckDB.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(DuckDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.DuckDB);

                        AssertDuckDBServices(services);
                    }
                },
                {
                    builder.WithKustoMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Kusto.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(KustoMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Kusto);

                        AssertKustoServices(services);
                    }
                },
                {
                    builder.WithMilvusMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Milvus.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MilvusMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Milvus);

                        AssertMilvusServices(services);
                    }
                },
                {
                    builder.WithMongoDBMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.MongoDB.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MongoDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.MongoDB);

                        AssertMongoDBServices(services);
                    }
                },
                {
                    builder.WithPineconeMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Pinecone.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(PineconeMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Pinecone);

                        AssertPineconeServices(services);
                    }
                },
                {
                    builder.WithPostgresMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Postgres.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(PostgresMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Postgres);

                        AssertPostgresServices(services);
                    }
                },
                {
                    builder.WithQdrantMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Qdrant.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(QdrantMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Qdrant);

                        AssertQdrantServices(services);
                    }
                },
                {
                    builder.WithRedisMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Redis.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(RedisMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Redis);

                        AssertRedisServices(services);
                    }
                },
                {
                    builder.WithSqliteMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Sqlite.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(SqliteMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Sqlite);

                        AssertSqliteServices(services);
                    }
                },
                {
                    builder.WithSqlServerMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.SqlServer.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(SqlServerMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.SqlServer);

                        AssertSqlServerServices(services);
                    }
                },
                {
                    builder.WithWeaviateMemoryStore,
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Weaviate.Should().NotBeNull();

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(WeaviateMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == options.Store!.Weaviate);

                        AssertWeaviateServices(services);
                    }
                }
            };
        }
    }

    public static TheoryData<Func<object, IMemoryBuilder>, IMemoryBuilder, object, Action> AddMemoryWithCustomOptions
    {
        get
        {
            var azureAISearch = new AzureAISearchMemoryStoreOptions();
            var azureCosmosDBMongoDB = new AzureCosmosDBMongoDBMemoryStoreOptions();
            var azureCosmosDBNoSQL = new AzureCosmosDBNoSQLMemoryStoreOptions();
            var chroma = new ChromaMemoryStoreOptions();
            var duckDB = new DuckDBMemoryStoreOptions();
            var kusto = new KustoMemoryStoreOptions();
            var milvus = new MilvusMemoryStoreOptions();
            var mongoDB = new MongoDBMemoryStoreOptions();
            var pinecone = new PineconeMemoryStoreOptions();
            var postgres = new PostgresMemoryStoreOptions();
            var qdrant = new QdrantMemoryStoreOptions();
            var redis = new RedisMemoryStoreOptions();
            var sqlite = new SqliteMemoryStoreOptions();
            var sqlServer = new SqlServerMemoryStoreOptions();
            var weaviate = new WeaviateMemoryStoreOptions();

            var options = new MemoryOptions { Store = new MemoryStoreOptions() };
            var services = new ServiceCollection();
            var builder = new MemoryBuilder(options, services);

            return new TheoryData<Func<object, IMemoryBuilder>, IMemoryBuilder, object, Action>
            {
                {
                    opt =>
                    {
                        // Act
                        options.Store.AzureAISearch = opt as AzureAISearchMemoryStoreOptions;
                        return builder.WithAzureAISearchMemoryStore();
                    },
                    builder,
                    azureAISearch,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureAISearch.Should().Be(azureAISearch);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureAISearchMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == azureAISearch);

                        AssertAzureAISearchServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.AzureCosmosDBMongoDB = opt as AzureCosmosDBMongoDBMemoryStoreOptions;
                        return builder.WithAzureCosmosDBMongoDBMemoryStore();
                    },
                    builder,
                    azureCosmosDBMongoDB,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureCosmosDBMongoDB.Should().Be(azureCosmosDBMongoDB);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureCosmosDBMongoDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == azureCosmosDBMongoDB);

                        AssertAzureCosmosDBMongoDBServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.AzureCosmosDBNoSQL = opt as AzureCosmosDBNoSQLMemoryStoreOptions;
                        return builder.WithAzureCosmosDBNoSQLMemoryStore();
                    },
                    builder,
                    azureCosmosDBNoSQL,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureCosmosDBNoSQL.Should().Be(azureCosmosDBNoSQL);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureCosmosDBNoSQLMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == azureCosmosDBNoSQL);

                        AssertAzureCosmosDBNoSQLServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Chroma = opt as ChromaMemoryStoreOptions;
                        return builder.WithChromaMemoryStore();
                    },
                    builder,
                    chroma,
                    () =>
                    {
                        // Assert
                        options.Store!.Chroma.Should().Be(chroma);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(ChromaMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == chroma);

                        AssertChromaServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.DuckDB = opt as DuckDBMemoryStoreOptions;
                        return builder.WithDuckDBMemoryStore();
                    },
                    builder,
                    duckDB,
                    () =>
                    {
                        // Assert
                        options.Store!.DuckDB.Should().Be(duckDB);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(DuckDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == duckDB);

                        AssertDuckDBServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Kusto = opt as KustoMemoryStoreOptions;
                        return builder.WithKustoMemoryStore();
                    },
                    builder,
                    kusto,
                    () =>
                    {
                        // Assert
                        options.Store!.Kusto.Should().Be(kusto);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(KustoMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == kusto);

                        AssertKustoServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Milvus = opt as MilvusMemoryStoreOptions;
                        return builder.WithMilvusMemoryStore();
                    },
                    builder,
                    milvus,
                    () =>
                    {
                        // Assert
                        options.Store!.Milvus.Should().Be(milvus);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MilvusMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == milvus);

                        AssertMilvusServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.MongoDB = opt as MongoDBMemoryStoreOptions;
                        return builder.WithMongoDBMemoryStore();
                    },
                    builder,
                    mongoDB,
                    () =>
                    {
                        // Assert
                        options.Store!.MongoDB.Should().Be(mongoDB);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MongoDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == mongoDB);

                        AssertMongoDBServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Pinecone = opt as PineconeMemoryStoreOptions;
                        return builder.WithPineconeMemoryStore();
                    },
                    builder,
                    pinecone,
                    () =>
                    {
                        // Assert
                        options.Store!.Pinecone.Should().Be(pinecone);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(PineconeMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == pinecone);

                        AssertPineconeServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Postgres = opt as PostgresMemoryStoreOptions;
                        return builder.WithPostgresMemoryStore();
                    },
                    builder,
                    postgres,
                    () =>
                    {
                        // Assert
                        options.Store!.Postgres.Should().Be(postgres);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(PostgresMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == postgres);

                        AssertPostgresServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Qdrant = opt as QdrantMemoryStoreOptions;
                        return builder.WithQdrantMemoryStore();
                    },
                    builder,
                    qdrant,
                    () =>
                    {
                        // Assert
                        options.Store!.Qdrant.Should().Be(qdrant);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(QdrantMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == qdrant);

                        AssertQdrantServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Redis = opt as RedisMemoryStoreOptions;
                        return builder.WithRedisMemoryStore();
                    },
                    builder,
                    redis,
                    () =>
                    {
                        // Assert
                        options.Store!.Redis.Should().Be(redis);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(RedisMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == redis);

                        AssertRedisServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Sqlite = opt as SqliteMemoryStoreOptions;
                        return builder.WithSqliteMemoryStore();
                    },
                    builder,
                    sqlite,
                    () =>
                    {
                        // Assert
                        options.Store!.Sqlite.Should().Be(sqlite);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(SqliteMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == sqlite);

                        AssertSqliteServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.SqlServer = opt as SqlServerMemoryStoreOptions;
                        return builder.WithSqlServerMemoryStore();
                    },
                    builder,
                    sqlServer,
                    () =>
                    {
                        // Assert
                        options.Store!.SqlServer.Should().Be(sqlServer);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(SqlServerMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == sqlServer);

                        AssertSqlServerServices(services);
                    }
                },
                {
                    opt =>
                    {
                        // Act
                        options.Store.Weaviate = opt as WeaviateMemoryStoreOptions;
                        return builder.WithWeaviateMemoryStore();
                    },
                    builder,
                    weaviate,
                    () =>
                    {
                        // Assert
                        options.Store!.Weaviate.Should().Be(weaviate);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(WeaviateMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance == weaviate);

                        AssertWeaviateServices(services);
                    }
                }
            };
        }
    }

    public static TheoryData<Func<IMemoryBuilder>, IMemoryBuilder, Action> AddMemoryWithOptionsAction
    {
        get
        {
            var azureAISearch = new AzureAISearchMemoryStoreOptions
            {
                Endpoint = "AzureAISearchMemoryStoreOptionsEndpoint",
                ApiKey = "AzureAISearchMemoryStoreOptionsApiKey"
            };
            var azureCosmosDBMongoDB = new AzureCosmosDBMongoDBMemoryStoreOptions
            {
                ConnectionString = "AzureCosmosDBMongoDBMemoryStoreOptionsConnectionString",
                DatabaseName = "AzureCosmosDBMongoDBMemoryStoreOptionsDatabaseName",
                Dimensions = 1024
            };
            var azureCosmosDBNoSQL = new AzureCosmosDBNoSQLMemoryStoreOptions
            {
                ConnectionString = "AzureCosmosDBNoSQLMemoryStoreOptionsConnectionString",
                DatabaseName = "AzureCosmosDBNoSQLMemoryStoreOptionsDatabaseName",
                Dimensions = 1024,
                VectorDataType = VectorDataType.Int8,
                VectorIndexType = VectorIndexType.QuantizedFlat,
                ApplicationName = "AzureCosmosDBNoSQLMemoryStoreOptionsApplicationName"
            };
            var chroma = new ChromaMemoryStoreOptions
            {
                Endpoint = "ChromaMemoryStoreOptionsEndpoint"
            };
            var duckDB = new DuckDBMemoryStoreOptions
            {
                Filename = "DuckDBMemoryStoreOptionsFilename",
                VectorSize = 1024
            };
            var kusto = new KustoMemoryStoreOptions
            {
                Database = "KustoMemoryStoreOptionsDatabase"
            };
            var milvus = new MilvusMemoryStoreOptions
            {
                Host = "MilvusMemoryStoreOptionsHost",
                Port = 1024,
                Ssl = true,
                Database = "MilvusMemoryStoreOptionsDatabase",
                IndexName = "MilvusMemoryStoreOptionsIndexName",
                VectorSize = 1024,
                MetricType = SimilarityMetricType.Substructure,
                ConsistencyLevel = ConsistencyLevel.Customized
            };
            var mongoDB = new MongoDBMemoryStoreOptions
            {
                ConnectionString = "MongoDBMemoryStoreOptionsConnectionString",
                DatabaseName = "MongoDBMemoryStoreOptionsDatabaseName",
                IndexName = "MongoDBMemoryStoreOptionsIndexName"
            };
            var pinecone = new PineconeMemoryStoreOptions
            {
                PineconeEnvironment = "PineconeMemoryStoreOptionsPineconeEnvironment",
                ApiKey = "PineconeMemoryStoreOptionsApiKey"
            };
            var postgres = new PostgresMemoryStoreOptions
            {
                ConnectionString = "PostgresMemoryStoreOptionsConnectionString",
                VectorSize = 1024,
                Schema = "PostgresMemoryStoreOptionsSchema"
            };
            var qdrant = new QdrantMemoryStoreOptions
            {
                Endpoint = "QdrantMemoryStoreOptionsEndpoint",
                VectorSize = 1024
            };
            var redis = new RedisMemoryStoreOptions
            {
                ConnectionString = "RedisMemoryStoreOptionsConnectionString",
                VectorSize = 1024,
                VectorIndexAlgorithm = VectorIndexAlgorithm.HNSW,
                VectorDistanceMetric = VectorDistanceMetric.COSINE,
                QueryDialect = 1
            };
            var sqlite = new SqliteMemoryStoreOptions
            {
                Filename = "SqliteMemoryStoreOptionsFilename"
            };
            var sqlServer = new SqlServerMemoryStoreOptions
            {
                ConnectionString = "SqlServerMemoryStoreOptionsConnectionString",
                Schema = "SqlServerMemoryStoreOptionsSchema"
            };
            var weaviate = new WeaviateMemoryStoreOptions
            {
                Endpoint = "WeaviateMemoryStoreOptionsEndpoint",
                ApiKey = "WeaviateMemoryStoreOptionsApiKey",
                ApiVersion = "WeaviateMemoryStoreOptionsApiVersion"
            };

            var options = new MemoryOptions();
            var services = new ServiceCollection();
            var builder = new MemoryBuilder(options, services);

            return new TheoryData<Func<IMemoryBuilder>, IMemoryBuilder, Action>
            {
                {
                    // Act
                    () => builder.WithAzureAISearchMemoryStore(opt =>
                    {
                        opt.Endpoint = azureAISearch.Endpoint;
                        opt.ApiKey = azureAISearch.ApiKey;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureAISearch.Should().NotBeNull();
                        options.Store!.AzureAISearch!.Endpoint.Should().Be(azureAISearch.Endpoint);
                        options.Store!.AzureAISearch!.ApiKey.Should().Be(azureAISearch.ApiKey);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureAISearchMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertAzureAISearchServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithAzureCosmosDBMongoDBMemoryStore(opt =>
                    {
                        opt.ConnectionString = azureCosmosDBMongoDB.ConnectionString;
                        opt.DatabaseName = azureCosmosDBMongoDB.DatabaseName;
                        opt.Dimensions = azureCosmosDBMongoDB.Dimensions;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureCosmosDBMongoDB.Should().NotBeNull();
                        options.Store!.AzureCosmosDBMongoDB!.ConnectionString.Should().Be(azureCosmosDBMongoDB.ConnectionString);
                        options.Store!.AzureCosmosDBMongoDB!.DatabaseName.Should().Be(azureCosmosDBMongoDB.DatabaseName);
                        options.Store!.AzureCosmosDBMongoDB!.Dimensions.Should().Be(azureCosmosDBMongoDB.Dimensions);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureCosmosDBMongoDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertAzureCosmosDBMongoDBServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithAzureCosmosDBNoSQLMemoryStore(opt =>
                    {
                        opt.ConnectionString = azureCosmosDBNoSQL.ConnectionString;
                        opt.DatabaseName = azureCosmosDBNoSQL.DatabaseName;
                        opt.Dimensions = azureCosmosDBNoSQL.Dimensions;
                        opt.VectorDataType = azureCosmosDBNoSQL.VectorDataType;
                        opt.VectorIndexType = azureCosmosDBNoSQL.VectorIndexType;
                        opt.ApplicationName = azureCosmosDBNoSQL.ApplicationName;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.AzureCosmosDBNoSQL.Should().NotBeNull();
                        options.Store!.AzureCosmosDBNoSQL!.ConnectionString.Should().Be(azureCosmosDBNoSQL.ConnectionString);
                        options.Store!.AzureCosmosDBNoSQL!.DatabaseName.Should().Be(azureCosmosDBNoSQL.DatabaseName);
                        options.Store!.AzureCosmosDBNoSQL!.Dimensions.Should().Be(azureCosmosDBNoSQL.Dimensions);
                        options.Store!.AzureCosmosDBNoSQL!.VectorDataType.Should().Be(azureCosmosDBNoSQL.VectorDataType);
                        options.Store!.AzureCosmosDBNoSQL!.VectorIndexType.Should().Be(azureCosmosDBNoSQL.VectorIndexType);
                        options.Store!.AzureCosmosDBNoSQL!.ApplicationName.Should().Be(azureCosmosDBNoSQL.ApplicationName);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(AzureCosmosDBNoSQLMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertAzureCosmosDBNoSQLServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithChromaMemoryStore(opt =>
                    {
                        opt.Endpoint = chroma.Endpoint;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Chroma.Should().NotBeNull();
                        options.Store!.Chroma!.Endpoint.Should().Be(chroma.Endpoint);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(ChromaMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertChromaServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithDuckDBMemoryStore(opt =>
                    {
                        opt.Filename = duckDB.Filename;
                        opt.VectorSize = duckDB.VectorSize;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.DuckDB.Should().NotBeNull();
                        options.Store!.DuckDB!.Filename.Should().Be(duckDB.Filename);
                        options.Store!.DuckDB!.VectorSize.Should().Be(duckDB.VectorSize);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(DuckDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertDuckDBServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithKustoMemoryStore(opt =>
                    {
                        opt.Database = kusto.Database;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Kusto.Should().NotBeNull();
                        options.Store!.Kusto!.Database.Should().Be(kusto.Database);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(KustoMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertKustoServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithMilvusMemoryStore(opt =>
                    {
                        opt.Host = milvus.Host;
                        opt.Port = milvus.Port;
                        opt.Ssl = milvus.Ssl;
                        opt.Database = milvus.Database;
                        opt.IndexName = milvus.IndexName;
                        opt.VectorSize = milvus.VectorSize;
                        opt.MetricType = milvus.MetricType;
                        opt.ConsistencyLevel = milvus.ConsistencyLevel;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Milvus.Should().NotBeNull();
                        options.Store!.Milvus!.Host.Should().Be(milvus.Host);
                        options.Store!.Milvus!.Port.Should().Be(milvus.Port);
                        options.Store!.Milvus!.Ssl.Should().Be(milvus.Ssl);
                        options.Store!.Milvus!.Database.Should().Be(milvus.Database);
                        options.Store!.Milvus!.IndexName.Should().Be(milvus.IndexName);
                        options.Store!.Milvus!.VectorSize.Should().Be(milvus.VectorSize);
                        options.Store!.Milvus!.MetricType.Should().Be(milvus.MetricType);
                        options.Store!.Milvus!.ConsistencyLevel.Should().Be(milvus.ConsistencyLevel);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MilvusMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertMilvusServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithMongoDBMemoryStore(opt =>
                    {
                        opt.ConnectionString = mongoDB.ConnectionString;
                        opt.DatabaseName = mongoDB.DatabaseName;
                        opt.IndexName = mongoDB.IndexName;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.MongoDB.Should().NotBeNull();
                        options.Store!.MongoDB!.ConnectionString.Should().Be(mongoDB.ConnectionString);
                        options.Store!.MongoDB!.DatabaseName.Should().Be(mongoDB.DatabaseName);
                        options.Store!.MongoDB!.IndexName.Should().Be(mongoDB.IndexName);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(MongoDBMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertMongoDBServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithPineconeMemoryStore(opt =>
                    {
                        opt.PineconeEnvironment = pinecone.PineconeEnvironment;
                        opt.ApiKey = pinecone.ApiKey;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Pinecone.Should().NotBeNull();
                        options.Store!.Pinecone!.PineconeEnvironment.Should().Be(pinecone.PineconeEnvironment);
                        options.Store!.Pinecone!.ApiKey.Should().Be(pinecone.ApiKey);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(PineconeMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertPineconeServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithPostgresMemoryStore(opt =>
                    {
                        opt.ConnectionString = postgres.ConnectionString;
                        opt.VectorSize = postgres.VectorSize;
                        opt.Schema = postgres.Schema;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Postgres.Should().NotBeNull();
                        options.Store!.Postgres!.ConnectionString.Should().Be(postgres.ConnectionString);
                        options.Store!.Postgres!.VectorSize.Should().Be(postgres.VectorSize);
                        options.Store!.Postgres!.Schema.Should().Be(postgres.Schema);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(PostgresMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertPostgresServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithQdrantMemoryStore(opt =>
                    {
                        opt.Endpoint = qdrant.Endpoint;
                        opt.VectorSize = qdrant.VectorSize;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Qdrant.Should().NotBeNull();
                        options.Store!.Qdrant!.Endpoint.Should().Be(qdrant.Endpoint);
                        options.Store!.Qdrant!.VectorSize.Should().Be(qdrant.VectorSize);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(QdrantMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertQdrantServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithRedisMemoryStore(opt =>
                    {
                        opt.ConnectionString = redis.ConnectionString;
                        opt.VectorSize = redis.VectorSize;
                        opt.VectorIndexAlgorithm = redis.VectorIndexAlgorithm;
                        opt.VectorDistanceMetric = redis.VectorDistanceMetric;
                        opt.QueryDialect = redis.QueryDialect;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Redis.Should().NotBeNull();
                        options.Store!.Redis!.ConnectionString.Should().Be(redis.ConnectionString);
                        options.Store!.Redis!.VectorSize.Should().Be(redis.VectorSize);
                        options.Store!.Redis!.VectorIndexAlgorithm.Should().Be(redis.VectorIndexAlgorithm);
                        options.Store!.Redis!.VectorDistanceMetric.Should().Be(redis.VectorDistanceMetric);
                        options.Store!.Redis!.QueryDialect.Should().Be(redis.QueryDialect);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(RedisMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertRedisServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithSqliteMemoryStore(opt =>
                    {
                        opt.Filename = sqlite.Filename;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Sqlite.Should().NotBeNull();
                        options.Store!.Sqlite!.Filename.Should().Be(sqlite.Filename);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(SqliteMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertSqliteServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithSqlServerMemoryStore(opt =>
                    {
                        opt.ConnectionString = sqlServer.ConnectionString;
                        opt.Schema = sqlServer.Schema;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.SqlServer.Should().NotBeNull();
                        options.Store!.SqlServer!.ConnectionString.Should().Be(sqlServer.ConnectionString);
                        options.Store!.SqlServer!.Schema.Should().Be(sqlServer.Schema);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(SqlServerMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertSqlServerServices(services);
                    }
                },
                {
                    // Act
                    () => builder.WithWeaviateMemoryStore(opt =>
                    {
                        opt.Endpoint = weaviate.Endpoint;
                        opt.ApiKey = weaviate.ApiKey;
                        opt.ApiVersion = weaviate.ApiVersion;
                    }),
                    builder,
                    () =>
                    {
                        // Assert
                        options.Store!.Weaviate.Should().NotBeNull();
                        options.Store!.Weaviate!.Endpoint.Should().Be(weaviate.Endpoint);
                        options.Store!.Weaviate!.ApiKey.Should().Be(weaviate.ApiKey);
                        options.Store!.Weaviate!.ApiVersion.Should().Be(weaviate.ApiVersion);

                        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                                      descriptor.ServiceType == typeof(WeaviateMemoryStoreOptions) &&
                                                                      descriptor.ImplementationInstance != null);

                        AssertWeaviateServices(services);
                    }
                }
            };
        }
    }

    private static void AssertAzureAISearchServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(AzureAISearchMemoryStoreFactory));
    }

    private static void AssertAzureCosmosDBMongoDBServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(AzureCosmosDBMongoDBMemoryStoreFactory));
    }

    private static void AssertAzureCosmosDBNoSQLServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(AzureCosmosDBNoSQLMemoryStoreFactory));
    }

    private static void AssertChromaServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(ChromaMemoryStoreFactory));
    }

    private static void AssertDuckDBServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(DuckDBMemoryStoreFactory));
    }

    private static void AssertKustoServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(KustoMemoryStoreFactory));
    }

    private static void AssertMilvusServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(MilvusMemoryStoreFactory));
    }

    private static void AssertMongoDBServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(MongoDBMemoryStoreFactory));
    }

    private static void AssertPineconeServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(PineconeMemoryStoreFactory));
    }

    private static void AssertPostgresServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(PostgresMemoryStoreFactory));
    }

    private static void AssertQdrantServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(QdrantMemoryStoreFactory));
    }

    private static void AssertRedisServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(RedisMemoryStoreFactory));
    }

    private static void AssertSqliteServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(SqliteMemoryStoreFactory));
    }

    private static void AssertSqlServerServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(SqlServerMemoryStoreFactory));
    }

    private static void AssertWeaviateServices(IServiceCollection services)
    {
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(WeaviateMemoryStoreFactory));
    }
}
