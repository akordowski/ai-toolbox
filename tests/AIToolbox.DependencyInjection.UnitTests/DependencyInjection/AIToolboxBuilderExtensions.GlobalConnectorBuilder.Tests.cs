using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class AIToolboxBuilderExtensionsGlobalConnectorBuilderTests
{
    private readonly Mock<Action<IGlobalConnectorBuilder>> _builderActionMock = new();
    private readonly Mock<Action<GlobalConnectorOptions>> _optionsActionMock = new();

    public static TheoryData<Action> ConfigureOptionsWithNullBuilder
    {
        get
        {
            IAIToolboxBuilder builder = null!;

            return
            [
                () => builder.ConfigureGlobalConnectorOptions(_ => { }),
                () => builder.ConfigureGlobalConnectorOptions(_ => { }, _ => { })
            ];
        }
    }

    public static TheoryData<Action, string> ConfigureOptionsWithNullParameters
    {
        get
        {
            Action<IGlobalConnectorBuilder> nullBuilderAction = null!;
            Action<GlobalConnectorOptions> nullOptionsAction = null!;

            var builder = new AIToolboxBuilder(new AIToolboxOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.ConfigureGlobalConnectorOptions(nullBuilderAction), "builderAction" },

                { () => builder.ConfigureGlobalConnectorOptions(nullBuilderAction, _ => { }), "builderAction" },
                { () => builder.ConfigureGlobalConnectorOptions(_ => { }, nullOptionsAction), "optionsAction" }
            };
        }
    }

    [Theory]
    [MemberData(nameof(ConfigureOptionsWithNullBuilder))]
    public void Should_Throw_On_Configure_Options_With_Null_Builder(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builder");
    }

    [Theory]
    [MemberData(nameof(ConfigureOptionsWithNullParameters))]
    public void Should_Throw_On_Configure_Options_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Configure_Options_With_Default_Options()
    {
        // Arrange
        var options = new AIToolboxOptions();
        var builder = CreateBuilder(options);

        // Act
        builder.ConfigureGlobalConnectorOptions(_builderActionMock.Object);

        // Assert
        options.GlobalConnectors.Should().NotBeNull();

        _builderActionMock.Verify(action => action(It.IsAny<IGlobalConnectorBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Configure_Options_With_Options_Action()
    {
        // Arrange
        var options = new AIToolboxOptions();
        var builder = CreateBuilder(options);

        // Act
        builder.ConfigureGlobalConnectorOptions(_builderActionMock.Object, _optionsActionMock.Object);

        // Assert
        options.GlobalConnectors.Should().NotBeNull();

        _builderActionMock.Verify(action => action(It.IsAny<IGlobalConnectorBuilder>()), Times.Once);
        _optionsActionMock.Verify(action => action(It.IsAny<GlobalConnectorOptions>()), Times.Once);
    }

    private static AIToolboxBuilder CreateBuilder(AIToolboxOptions options) =>
        new(options, new ServiceCollection());
}
