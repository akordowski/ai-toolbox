using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class AzureAISearchMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_AzureAISearchMemoryStore_With_Options()
    {
        // Arrange
        var options = new AzureAISearchMemoryStoreOptions();

        // Act
        var result = Builder.IncludeAzureAISearchMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.AzureAISearch.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureAISearchMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            AzureAISearch = new AzureAISearchMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeAzureAISearchMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.AzureAISearch.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureAISearchMemoryStore_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeAzureAISearchMemoryStore(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.AzureAISearch!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_AzureAISearchMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeAzureAISearchMemoryStore((Action<AzureAISearchMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(AzureAISearchMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
