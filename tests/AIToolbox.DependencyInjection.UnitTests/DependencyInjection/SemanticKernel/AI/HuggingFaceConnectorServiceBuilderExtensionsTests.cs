using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class HuggingFaceConnectorServiceBuilderExtensionsTests : BaseConnectorServiceBuilderExtensionsTests
{
    [Fact]
    public void Should_Include_HuggingFaceConnector_With_Options()
    {
        // Arrange
        var options = new GlobalHuggingFaceOptions();

        // Act
        var result = Builder.IncludeHuggingFaceConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.HuggingFace.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_HuggingFaceConnector_With_Null_Options()
    {
        // Arrange
        Options.HuggingFace = new GlobalHuggingFaceOptions();

        // Act
        var result = Builder.IncludeHuggingFaceConnector();

        // Assert
        result.Should().Be(Builder);
        Options.HuggingFace.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_HuggingFaceConnector_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeHuggingFaceConnector(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.HuggingFace!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_HuggingFaceConnector_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeHuggingFaceConnector((Action<GlobalHuggingFaceOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(HuggingFaceKernelBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
