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
        _builder = new AIToolboxBuilder(_options, _services);
    }

    public static TheoryData<Action, string> ConstructWithInvalidParameters =>
        new()
        {
            { () => _ = new AIToolboxBuilder(null!, null!), "options" },
            { () => _ = new AIToolboxBuilder(new AIToolboxOptions(), null!), "services" }
        };

    [Theory]
    [MemberData(nameof(ConstructWithInvalidParameters))]
    public void Should_Throw_On_Construct_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        var builder = new AIToolboxBuilder(_options, _services);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(_services);
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
