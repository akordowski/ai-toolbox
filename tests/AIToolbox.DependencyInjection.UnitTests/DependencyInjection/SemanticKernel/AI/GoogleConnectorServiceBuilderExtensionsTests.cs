using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class GoogleConnectorServiceBuilderExtensionsTests : BaseConnectorServiceBuilderExtensionsTests
{
    [Fact]
    public void Should_Include_GoogleConnector_With_Options()
    {
        // Arrange
        var options = new GoogleConnectorOptions();

        // Act
        var result = Builder.IncludeGoogleConnector(options);

        // Assert
        result.Should().Be(Builder);
        Options.Google.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_GoogleConnector_With_Null_Options()
    {
        // Arrange
        Options.Google = new GoogleConnectorOptions();

        // Act
        var result = Builder.IncludeGoogleConnector();

        // Assert
        result.Should().Be(Builder);
        Options.Google.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_GoogleConnector_With_Options_Action()
    {
        // Act
        const string apiKey = "ApiKey";
        var result = Builder.IncludeGoogleConnector(options => options.ApiKey = apiKey);

        // Assert
        result.Should().Be(Builder);
        Options.Google!.ApiKey.Should().Be(apiKey);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_GoogleConnector_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeGoogleConnector((Action<GoogleConnectorOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                      descriptor.ImplementationType == typeof(GoogleKernelBuilderConfigurator) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
