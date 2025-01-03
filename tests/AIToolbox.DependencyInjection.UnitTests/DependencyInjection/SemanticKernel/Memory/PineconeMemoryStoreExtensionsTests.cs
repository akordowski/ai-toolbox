using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class PineconeMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_PineconeMemoryStore_With_Options()
    {
        // Arrange
        var options = new PineconeMemoryStoreOptions();

        // Act
        var result = Builder.IncludePineconeMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Pinecone.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_PineconeMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Pinecone = new PineconeMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludePineconeMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Pinecone.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_PineconeMemoryStore_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludePineconeMemoryStore(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Pinecone!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_PineconeMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludePineconeMemoryStore((Action<PineconeMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(PineconeMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
