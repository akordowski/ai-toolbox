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
    public void Should_Throw_Exception_When_Builder_Action_Is_Null()
    {
        // Act
        var act = () => _builder.ConfigureGlobalConnectorOptions(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builderAction");
    }

    [Fact]
    public void Should_Configure_Global_Connectors_With_Default_Options()
    {
        // Arrange
        _options.GlobalConnectors = null;
        var builder = new AIToolboxBuilder(_options, _services);

        // Act
        builder.ConfigureGlobalConnectorOptions(_ => { });

        // Assert
        _options.GlobalConnectors.Should().NotBeNull();
    }

    [Fact]
    public void Should_Configure_Global_Connectors_With_Custom_Options()
    {
        // Arrange
        var globalConnectorOptions = new GlobalConnectorOptions();
        _options.GlobalConnectors = globalConnectorOptions;
        var builder = new AIToolboxBuilder(_options, _services);

        // Act
        builder.ConfigureGlobalConnectorOptions(_ => { });

        // Assert
        _options.GlobalConnectors.Should().Be(globalConnectorOptions);
    }
}
