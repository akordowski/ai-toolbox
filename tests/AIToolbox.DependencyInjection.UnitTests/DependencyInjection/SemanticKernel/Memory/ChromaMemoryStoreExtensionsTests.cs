using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class ChromaMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_ChromaMemoryStore_With_Options()
    {
        // Arrange
        var options = new ChromaMemoryStoreOptions();

        // Act
        var result = Builder.IncludeChromaMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Chroma.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_ChromaMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Chroma = new ChromaMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeChromaMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Chroma.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_ChromaMemoryStore_With_Options_Action()
    {
        // Act
        const string endpoint = "Endpoint";
        var result = Builder.IncludeChromaMemoryStore(options => options.Endpoint = endpoint);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Chroma!.Endpoint.Should().Be(endpoint);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_ChromaMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeChromaMemoryStore((Action<ChromaMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(ChromaMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
