using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class AzureCosmosDBMongoDBMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_AzureCosmosDBMongoDBMemoryStore_With_Options()
    {
        // Arrange
        var options = new AzureCosmosDBMongoDBMemoryStoreOptions();

        // Act
        var result = Builder.IncludeAzureCosmosDBMongoDBMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.AzureCosmosDBMongoDB.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureCosmosDBMongoDBMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            AzureCosmosDBMongoDB = new AzureCosmosDBMongoDBMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeAzureCosmosDBMongoDBMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.AzureCosmosDBMongoDB.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureCosmosDBMongoDBMemoryStore_With_Options_Action()
    {
        // Act
        const string connectionString = "ConnectionString";
        var result = Builder.IncludeAzureCosmosDBMongoDBMemoryStore(options => options.ConnectionString = connectionString);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.AzureCosmosDBMongoDB!.ConnectionString.Should().Be(connectionString);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_AzureCosmosDBMongoDBMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeAzureCosmosDBMongoDBMemoryStore((Action<AzureCosmosDBMongoDBMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(AzureCosmosDBMongoDBMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
