using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.TestHelper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTests : BaseTestWithFixture<AIToolboxFixture>
{
    public SemanticKernelBuilderTests(AIToolboxFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public void Should_Get_Services_By_Default_Options()
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
    public void Should_Configure_By_Options_Action(Action<IAIToolboxBuilder, SemanticKernelOptions> act)
    {
        // Arrange
        var host = Fixture.GetHost((_, services) =>
            services.AddAIToolbox(aiToolbox => act(aiToolbox, TestOptions.AIToolbox.SemanticKernel!)));

        // Assert
        AssertServices(host.Services);
    }

    [Fact]
    public void Should_Get_Services_By_Config_File()
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

        services.GetService<KernelOptions>().Should().BeEquivalentTo(kernelOptions);
        services.GetService<IKernelProvider>().Should().NotBeNull();

        services.GetService<MemoryOptions>().Should().BeEquivalentTo(memoryOptions);
        services.GetService<IMemoryProvider>().Should().NotBeNull();

        services.GetService<AzureOpenAIOptions>().Should().BeEquivalentTo(connectorOptions.AzureOpenAI);
        services.GetService<GoogleOptions>().Should().BeEquivalentTo(connectorOptions.Google);
        services.GetService<HuggingFaceOptions>().Should().BeEquivalentTo(connectorOptions.HuggingFace);
        services.GetService<MistralAIOptions>().Should().BeEquivalentTo(connectorOptions.MistralAI);
        services.GetService<OllamaOptions>().Should().BeEquivalentTo(connectorOptions.Ollama);
        services.GetService<OpenAIOptions>().Should().BeEquivalentTo(connectorOptions.OpenAI);
        services.GetService<VertexAIOptions>().Should().BeEquivalentTo(connectorOptions.VertexAI);

        services.GetService<AzureAISearchMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.AzureAISearch);
        services.GetService<AzureCosmosDBMongoDBMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.AzureCosmosDBMongoDB);
        services.GetService<AzureCosmosDBNoSQLMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.AzureCosmosDBNoSQL);
        services.GetService<ChromaMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Chroma);
        services.GetService<DuckDBMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.DuckDB);
        services.GetService<KustoMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Kusto);
        services.GetService<MilvusMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Milvus);
        services.GetService<MongoDBMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.MongoDB);
        services.GetService<PineconeMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Pinecone);
        services.GetService<PostgresMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Postgres);
        services.GetService<QdrantMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Qdrant);
        services.GetService<RedisMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Redis);
        services.GetService<SqliteMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Sqlite);
        services.GetService<SqlServerMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.SqlServer);
        services.GetService<WeaviateMemoryStoreOptions>().Should().BeEquivalentTo(memoryStoreOptions.Weaviate);

        var kernelBuilderConfigurators = services.GetServices<IKernelBuilderConfigurator>().ToList();
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is AzureOpenAIKernelBuilderConfigurator);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is GoogleKernelBuilderConfigurator);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is HuggingFaceKernelBuilderConfigurator);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is MistralAIKernelBuilderConfigurator);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is OllamaKernelBuilderConfigurator);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is OpenAIKernelBuilderConfigurator);
        kernelBuilderConfigurators.Should().ContainSingle(configurator => configurator is VertexAIKernelBuilderConfigurator);

        var memoryBuilderConfigurators = services.GetServices<IMemoryBuilderConfigurator>().ToList();
        memoryBuilderConfigurators.Should().ContainSingle(configurator => configurator is AzureOpenAIMemoryBuilderConfigurator);
        memoryBuilderConfigurators.Should().ContainSingle(configurator => configurator is OllamaMemoryBuilderConfigurator);
        memoryBuilderConfigurators.Should().ContainSingle(configurator => configurator is OpenAIMemoryBuilderConfigurator);
    }
}
