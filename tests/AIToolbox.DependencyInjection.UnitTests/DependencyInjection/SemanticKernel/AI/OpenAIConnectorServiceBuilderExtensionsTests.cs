using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class OpenAIConnectorServiceBuilderExtensionsTests : BaseConnectorServiceBuilderExtensionsTests
{
    [Fact]
    public void Should_Include_OpenAIConnector_With_Options()
    {
        // Arrange
        var options = new GlobalOpenAIOptions();

        // Act
        var result = Builder.IncludeOpenAIConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.OpenAI.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_OpenAIConnector_With_Null_Options()
    {
        // Arrange
        Options.OpenAI = new GlobalOpenAIOptions();

        // Act
        var result = Builder.IncludeOpenAIConnector();

        // Assert
        result.Should().Be(Builder);
        Options.OpenAI.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_OpenAIConnector_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeOpenAIConnector(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.OpenAI!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_OpenAIConnector_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeOpenAIConnector((Action<GlobalOpenAIOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OpenAIKernelBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(OpenAIMemoryBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
