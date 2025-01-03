using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class QdrantMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_QdrantMemoryStore_With_Options()
    {
        // Arrange
        var options = new QdrantMemoryStoreOptions();

        // Act
        var result = Builder.IncludeQdrantMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Qdrant.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_QdrantMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Qdrant = new QdrantMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeQdrantMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Qdrant.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_QdrantMemoryStore_With_Options_Action()
    {
        // Act
        const string endpoint = "Endpoint";
        var result = Builder.IncludeQdrantMemoryStore(options => options.Endpoint = endpoint);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Qdrant!.Endpoint.Should().Be(endpoint);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_QdrantMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeQdrantMemoryStore((Action<QdrantMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(QdrantMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
