using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class OllamaConnectorServiceBuilderExtensionsTests : BaseConnectorServiceBuilderExtensionsTests
{
    [Fact]
    public void Should_Include_OllamaConnector_With_Options()
    {
        // Arrange
        var options = new OllamaConnectorOptions();

        // Act
        var result = Builder.IncludeOllamaConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Ollama.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_OllamaConnector_With_Null_Options()
    {
        // Arrange
        Options.Ollama = new OllamaConnectorOptions();

        // Act
        var result = Builder.IncludeOllamaConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Ollama.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_OllamaConnector_With_Options_Action()
    {
        // Act
        const string endpoint = "Endpoint";
        var result = Builder.IncludeOllamaConnector(options => options.Endpoint = endpoint);

        // Assert
        result.Should().Be(Builder);
        Options.Ollama!.Endpoint.Should().Be(endpoint);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_OllamaConnector_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeOllamaConnector((Action<OllamaConnectorOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OllamaKernelBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OllamaMemoryBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
