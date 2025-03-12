using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.TestHelper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Xunit.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTests : BaseTestWithFixture<AIToolboxFixture>
{
    public SemanticKernelBuilderTests(AIToolboxFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public void Should_Add_Services_By_Default_Options()
    {
        var host = Fixture.GetHost((_, services) =>
        {
            services.AddAIToolbox(
                aiToolbox => aiToolbox.AddSemanticKernel(semanticKernel =>
                    semanticKernel
                        .AddKernel(TestBuilder.AddKernelMethods)
                        .AddMemory(TestBuilder.AddMemoryMethods)),
                options => options.SemanticKernel = TestOptions.AIToolbox.SemanticKernel);
        });

        // Assert
        AssertServices(host.Services);
    }

    [Theory]
    [MemberData(nameof(SemanticKernelBuilderTestData.ConfigureByOptionsAction), MemberType = typeof(SemanticKernelBuilderTestData))]
    public void Should_Add_Services_By_Options_Action(Action<IAIToolboxBuilder, SemanticKernelOptions> act)
    {
        // Arrange
        var host = Fixture.GetHost((_, services) =>
            services.AddAIToolbox(aiToolbox => act(aiToolbox, TestOptions.AIToolbox.SemanticKernel!)));

        // Assert
        AssertServices(host.Services);
    }

    [Fact]
    public void Should_Add_Services_By_Config_File()
    {
        var host = Fixture.GetHost(
            (context, services) =>
                services.AddAIToolbox(
                    aiToolbox => aiToolbox.AddSemanticKernel(semanticKernel =>
                        semanticKernel
                            .AddKernel(TestBuilder.AddKernelMethods)
                            .AddMemory(TestBuilder.AddMemoryMethods)),
                    context.Configuration),
            true);

        // Assert
        AssertServices(host.Services);
    }

    private static void AssertServices(IServiceProvider services)
    {
        var semanticKernelOptions = TestOptions.AIToolbox.SemanticKernel!;
        var kernelOptions = semanticKernelOptions.Kernel!;
        var memoryOptions = semanticKernelOptions.Memory!;
        var connectorOptions = kernelOptions.Connectors!;
        var memoryStoreOptions = memoryOptions.Store!;

        // KernelBuilder
        services.GetService<KernelOptions>().Should().BeEquivalentTo(kernelOptions);
        services.GetService<IKernelProvider>().Should().NotBeNull();

        // MemoryBuilder
        services.GetService<MemoryOptions>().Should().BeEquivalentTo(memoryOptions);
        services.GetService<IMemoryProvider>().Should().NotBeNull();

        // --------------------------------------------------
        // Connectors
        // --------------------------------------------------

        var kernelBuilderConfigurators = services.GetServices<IKernelBuilderConfigurator>().ToList();
        var memoryBuilderConfigurators = services.GetServices<IMemoryBuilderConfigurator>().ToList();

        // AzureOpenAI
        services.GetService<AzureOpenAIOptions>().Should().BeEquivalentTo(connectorOptions.AzureOpenAI);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is AzureOpenAIKernelBuilderConfigurator);
        memoryBuilderConfigurators.Should().ContainSingle(configurator => configurator is AzureOpenAIMemoryBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(AzureOpenAIChatCompletionService)).Should().BeOfType<AzureOpenAIPromptExecutionSettingsMapper>();

        // Google
        services.GetService<GoogleOptions>().Should().BeEquivalentTo(connectorOptions.Google);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is GoogleKernelBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(GoogleAIGeminiChatCompletionService)).Should().BeOfType<GeminiPromptExecutionSettingsMapper>();

        // HuggingFace
        services.GetService<HuggingFaceOptions>().Should().BeEquivalentTo(connectorOptions.HuggingFace);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is HuggingFaceKernelBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(HuggingFaceChatCompletionService)).Should().BeOfType<HuggingFacePromptExecutionSettingsMapper>();

        // MistralAI
        services.GetService<MistralAIOptions>().Should().BeEquivalentTo(connectorOptions.MistralAI);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is MistralAIKernelBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(MistralAIChatCompletionService)).Should().BeOfType<MistralAIPromptExecutionSettingsMapper>();

        // Ollama
        services.GetService<OllamaOptions>().Should().BeEquivalentTo(connectorOptions.Ollama);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is OllamaKernelBuilderConfigurator);
        memoryBuilderConfigurators.Should().ContainSingle(configurator => configurator is OllamaMemoryBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(OllamaChatCompletionService)).Should().BeOfType<OllamaPromptExecutionSettingsMapper>();

        // OpenAI
        services.GetService<OpenAIOptions>().Should().BeEquivalentTo(connectorOptions.OpenAI);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is OpenAIKernelBuilderConfigurator);
        memoryBuilderConfigurators.Should().ContainSingle(configurator => configurator is OpenAIMemoryBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(OpenAIChatCompletionService)).Should().BeOfType<OpenAIPromptExecutionSettingsMapper>();
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(OpenAIAudioToTextService)).Should().BeOfType<OpenAIAudioToTextExecutionSettingsMapper>();
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(OpenAITextToAudioService)).Should().BeOfType<OpenAITextToAudioExecutionSettingsMapper>();

        // VertexA
        services.GetService<VertexAIOptions>().Should().BeEquivalentTo(connectorOptions.VertexAI);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is VertexAIKernelBuilderConfigurator);
        services.GetKeyedService<IPromptExecutionSettingsMapper>(typeof(VertexAIGeminiChatCompletionService)).Should().BeOfType<GeminiPromptExecutionSettingsMapper>();

        // --------------------------------------------------
        // Memory Stores
        // --------------------------------------------------

        // AzureAISearch
        services.GetService<AzureAISearchMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.AzureAISearch);

        // AzureCosmosDBMongoDB
        services.GetService<AzureCosmosDBMongoDBMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.AzureCosmosDBMongoDB);

        // AzureCosmosDBNoSQL
        services.GetService<AzureCosmosDBNoSQLMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.AzureCosmosDBNoSQL);

        // Chroma
        services.GetService<ChromaMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Chroma);

        // DuckDB
        services.GetService<DuckDBMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.DuckDB);

        // Kusto
        services.GetService<KustoMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Kusto);

        // Milvus
        services.GetService<MilvusMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Milvus);

        // MongoDB
        services.GetService<MongoDBMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.MongoDB);

        // Pinecone
        services.GetService<PineconeMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Pinecone);

        // Postgres
        services.GetService<PostgresMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Postgres);

        // Qdrant
        services.GetService<QdrantMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Qdrant);

        // Redis
        services.GetService<RedisMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Redis);

        // Sqlite
        services.GetService<SqliteMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Sqlite);

        // SqlServer
        services.GetService<SqlServerMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.SqlServer);

        // Weaviate
        services.GetService<WeaviateMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Weaviate);
    }
}
