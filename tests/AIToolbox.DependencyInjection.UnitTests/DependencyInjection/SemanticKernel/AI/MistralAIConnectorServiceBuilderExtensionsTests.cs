using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class MistralAIConnectorServiceBuilderExtensionsTests : BaseConnectorServiceBuilderExtensionsTests
{
    [Fact]
    public void Should_Include_MistralAIConnector_With_Options()
    {
        // Arrange
        var options = new MistralAIConnectorOptions();

        // Act
        var result = Builder.IncludeMistralAIConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.MistralAI.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_MistralAIConnector_With_Null_Options()
    {
        // Arrange
        Options.MistralAI = new MistralAIConnectorOptions();

        // Act
        var result = Builder.IncludeMistralAIConnector();

        // Assert
        result.Should().Be(Builder);
        Options.MistralAI.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_MistralAIConnector_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeMistralAIConnector(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.MistralAI!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_MistralAIConnector_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeMistralAIConnector((Action<MistralAIConnectorOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(MistralAIKernelBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
