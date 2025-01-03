using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class AzureCosmosDBNoSQLMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_AzureCosmosDBNoSQLMemoryStore_With_Options()
    {
        // Arrange
        var options = new AzureCosmosDBNoSQLMemoryStoreOptions();

        // Act
        var result = Builder.IncludeAzureCosmosDBNoSQLMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.AzureCosmosDBNoSQL.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureCosmosDBNoSQLMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            AzureCosmosDBNoSQL = new AzureCosmosDBNoSQLMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeAzureCosmosDBNoSQLMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.AzureCosmosDBNoSQL.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureCosmosDBNoSQLMemoryStore_With_Options_Action()
    {
        // Act
        const string connectionString = "ConnectionString";
        var result = Builder.IncludeAzureCosmosDBNoSQLMemoryStore(options => options.ConnectionString = connectionString);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.AzureCosmosDBNoSQL!.ConnectionString.Should().Be(connectionString);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_AzureCosmosDBNoSQLMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeAzureCosmosDBNoSQLMemoryStore((Action<AzureCosmosDBNoSQLMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(AzureCosmosDBNoSQLMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
