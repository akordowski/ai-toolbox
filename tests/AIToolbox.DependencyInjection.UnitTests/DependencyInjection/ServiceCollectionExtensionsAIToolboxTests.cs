using AIToolbox.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class ServiceCollectionExtensionsAIToolboxTests
{
    private static readonly ServiceCollection Services = [];
    private static readonly ServiceCollection NullServices = null!;
    private static readonly AIToolboxOptions Options = new();
    private static readonly Action<IAIToolboxBuilder> NullBuilderAction = null!;
    private static readonly Mock<Action<IAIToolboxBuilder>> BuilderActionMock = new();
    private static readonly Mock<Action<AIToolboxOptions>> OptionsActionMock = new();
    private static readonly Mock<IConfiguration> ConfigurationMock = new();

    public ServiceCollectionExtensionsAIToolboxTests()
    {
        BuilderActionMock.Reset();
        OptionsActionMock.Reset();
        ConfigurationMock.Reset();
    }

    public static TheoryData<Action> AddAIToolboxWithNullServices =>
    [
        () => NullServices.AddAIToolbox(BuilderActionMock.Object),
        () => NullServices.AddAIToolbox(BuilderActionMock.Object, Options),
        () => NullServices.AddAIToolbox(BuilderActionMock.Object, OptionsActionMock.Object),
        () => NullServices.AddAIToolbox(BuilderActionMock.Object, ConfigurationMock.Object)
    ];

    public static TheoryData<Action, string, bool> AddAIToolboxWithParameters =>
        new()
        {
            { () => Services.AddAIToolbox(NullBuilderAction), "builderAction", true },

            { () => Services.AddAIToolbox(NullBuilderAction, Options), "builderAction", true },
            { () => Services.AddAIToolbox(BuilderActionMock.Object, (AIToolboxOptions)null!), "options", true },

            { () => Services.AddAIToolbox(NullBuilderAction, OptionsActionMock.Object), "builderAction", true },
            { () => Services.AddAIToolbox(BuilderActionMock.Object, (Action<AIToolboxOptions>)null!), "optionsAction", true },

            { () => Services.AddAIToolbox(NullBuilderAction, ConfigurationMock.Object), "builderAction", true },
            { () => Services.AddAIToolbox(BuilderActionMock.Object, (IConfiguration)null!), "configuration", true },
            { () => Services.AddAIToolbox(BuilderActionMock.Object, ConfigurationMock.Object, null!), "sectionName", true },
            { () => Services.AddAIToolbox(BuilderActionMock.Object, ConfigurationMock.Object, ""), "sectionName", false },
            { () => Services.AddAIToolbox(BuilderActionMock.Object, ConfigurationMock.Object, " "), "sectionName", false }
        };

    [Theory]
    [MemberData(nameof(AddAIToolboxWithNullServices))]
    public void Should_Throw_On_Add_AIToolbox_When_Services_Is_Null(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    [Theory]
    [MemberData(nameof(AddAIToolboxWithParameters))]
    public void Should_Throw_On_Add_AIToolbox_With_Invalid_Parameters(Action act, string parameterName, bool isNullException)
    {
        // Assert
        if (isNullException)
        {
            act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
        }
        else
        {
            act.Should().Throw<ArgumentException>().WithParameterName(parameterName);
        }
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Default_Options()
    {
        // Act
        Services.AddAIToolbox(BuilderActionMock.Object);

        // Assert
        BuilderActionMock.Verify(action => action(It.IsAny<IAIToolboxBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Custom_Options()
    {
        // Act
        Services.AddAIToolbox(BuilderActionMock.Object, Options);

        // Assert
        BuilderActionMock.Verify(action => action(It.IsAny<IAIToolboxBuilder>()), Times.Once);
        BuilderActionMock.Verify(action => action(It.Is<IAIToolboxBuilder>(builder => builder.BuilderFactory.Options == Options)), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Options_Action()
    {
        // Act
        Services.AddAIToolbox(BuilderActionMock.Object, OptionsActionMock.Object);

        // Assert
        BuilderActionMock.Verify(action => action(It.IsAny<IAIToolboxBuilder>()), Times.Once);
        OptionsActionMock.Verify(action => action(It.IsAny<AIToolboxOptions>()), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Configuration_And_Default_Section_Name()
    {
        // Arrange
        var configuration = GetConfiguration("AIToolbox");

        // Act
        Services.AddAIToolbox(BuilderActionMock.Object, configuration);

        // Assert
        BuilderActionMock.Verify(action => action(It.IsAny<IAIToolboxBuilder>()), Times.Once);
        BuilderActionMock.Verify(action => action(It.Is<IAIToolboxBuilder>(builder => builder.BuilderFactory.Options.Connectors != null)), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Configuration_And_Custom_Section_Name()
    {
        // Arrange
        const string sectionName = "ConfigSection";
        var configuration = GetConfiguration(sectionName);

        // Act
        Services.AddAIToolbox(BuilderActionMock.Object, configuration, sectionName);

        // Assert
        BuilderActionMock.Verify(action => action(It.IsAny<IAIToolboxBuilder>()), Times.Once);
        BuilderActionMock.Verify(action => action(It.Is<IAIToolboxBuilder>(builder => builder.BuilderFactory.Options.Connectors != null)), Times.Once);
    }

    private static IConfiguration GetConfiguration(string section) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{section}:Connectors:AzureOpenAI"] = null
            }!)
            .Build();
}
