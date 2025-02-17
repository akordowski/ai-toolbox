using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class AzureOpenAIConnectorServiceBuilderExtensionsTests : BaseConnectorServiceBuilderExtensionsTests
{
    [Fact]
    public void Should_Include_AzureOpenAIConnector_With_Options()
    {
        // Arrange
        var options = new GlobalAzureOpenAIOptions();

        // Act
        var result = Builder.IncludeAzureOpenAIConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.AzureOpenAI.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureOpenAIConnector_With_Null_Options()
    {
        // Arrange
        Options.AzureOpenAI = new GlobalAzureOpenAIOptions();

        // Act
        var result = Builder.IncludeAzureOpenAIConnector();

        // Assert
        result.Should().Be(Builder);
        Options.AzureOpenAI.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_AzureOpenAIConnector_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeAzureOpenAIConnector(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.AzureOpenAI!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_AzureOpenAIConnector_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeAzureOpenAIConnector((Action<GlobalAzureOpenAIOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(AzureOpenAIKernelBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(AzureOpenAIMemoryBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
