using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class AIToolboxBuilderTests
{
    private readonly AIToolboxOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly AIToolboxBuilder _builder;

    public AIToolboxBuilderTests()
    {
        var builderFactory = new BuilderFactory(_options, _services);
        _builder = new AIToolboxBuilder(builderFactory);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Builder_Factory()
    {
        // Act
        var act = () => new AIToolboxBuilder(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builderFactory");
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        var builderFactory = new BuilderFactory(_options, _services);
        var builder = new AIToolboxBuilder(builderFactory);

        // Assert
        builder.BuilderFactory.Should().Be(builderFactory);
    }

    [Fact]
    public void Should_Configure_Connectors_With_Null_Default_Options()
    {
        // Arrange
        _options.GlobalConnectors = null;
        var connectorOptions = new GlobalConnectorOptions();

        // Act
        _builder.ConfigureConnectors(connectorOptions);

        // Assert
        _options.GlobalConnectors.Should().Be(connectorOptions);
    }

    [Fact]
    public void Should_Configure_Connectors_With_Default_Options()
    {
        // Arrange
        _options.GlobalConnectors = new GlobalConnectorOptions();
        var connectorOptions = new GlobalConnectorOptions();

        // Act
        _builder.ConfigureConnectors(connectorOptions);

        // Assert
        _options.GlobalConnectors.Should().Be(connectorOptions);
    }

    [Fact]
    public void Should_Throw_Exception_When_Options_Action_Is_Null()
    {
        // Act
        var act = () => _builder.ConfigureConnectors((Action<GlobalConnectorOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Should_Configure_Connectors_With_Options_Action(bool hasConnectorOptions)
    {
        // Arrange
        var connectorOptions = hasConnectorOptions
            ? new GlobalConnectorOptions()
            : null;
        var azureOpenAIOptions = new GlobalAzureOpenAIOptions { ApiKey = "test" };
        _options.GlobalConnectors = connectorOptions;

        // Act
        _builder.ConfigureConnectors(opt => opt.AzureOpenAI = azureOpenAIOptions);

        // Assert
        if (hasConnectorOptions)
        {
            _options.GlobalConnectors.Should().Be(connectorOptions);
        }
        else
        {
            _options.GlobalConnectors.Should().NotBeNull();
        }

        _options.GlobalConnectors!.AzureOpenAI.Should().NotBeNull();
        _options.GlobalConnectors!.AzureOpenAI.Should().Be(azureOpenAIOptions);
    }
}
