using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class WeaviateMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_WeaviateMemoryStore_With_Options()
    {
        // Arrange
        var options = new WeaviateMemoryStoreOptions();

        // Act
        var result = Builder.IncludeWeaviateMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Weaviate.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_WeaviateMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Weaviate = new WeaviateMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeWeaviateMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Weaviate.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_WeaviateMemoryStore_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeWeaviateMemoryStore(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Weaviate!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_WeaviateMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeWeaviateMemoryStore((Action<WeaviateMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(WeaviateMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
