using AIToolbox.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace AIToolbox;

public class AIToolboxBuilderServiceCollectionExtensionsTests
{
    private static readonly Type TypeArgumentException = typeof(ArgumentException);
    private static readonly Type TypeArgumentNullException = typeof(ArgumentNullException);

    private readonly ServiceCollection _services = [];
    private readonly Mock<Action<AIToolboxBuilder>> _builderActionMock = new();
    private readonly Mock<Action<AIToolboxOptions>> _optionsActionMock = new();

    public static TheoryData<Action> AddAIToolboxWithNullServices
    {
        get
        {
            ServiceCollection services = null!;

            return
            [
                () => services.AddAIToolbox(_ => { }),
                () => services.AddAIToolbox(_ => { }, _ => { }),
                () => services.AddAIToolbox(_ => { }, Mock.Of<IConfiguration>())
            ];
        }
    }

    public static TheoryData<Action, string, Type> AddAIToolboxWithNullParameters
    {
        get
        {
            Action<AIToolboxBuilder> nullBuilderAction = null!;
            Action<AIToolboxOptions> nullOptionsAction = null!;
            IConfiguration nullConfiguration = null!;

            var services = new ServiceCollection();
            var configuration = Mock.Of<IConfiguration>();

            return new TheoryData<Action, string, Type>
            {
                { () => services.AddAIToolbox(nullBuilderAction), "builderAction", TypeArgumentNullException },

                { () => services.AddAIToolbox(nullBuilderAction, _ => { }), "builderAction", TypeArgumentNullException },
                { () => services.AddAIToolbox(_ => { }, nullOptionsAction), "optionsAction", TypeArgumentNullException },

                { () => services.AddAIToolbox(nullBuilderAction, configuration), "builderAction", TypeArgumentNullException },
                { () => services.AddAIToolbox(_ => { }, nullConfiguration), "configuration", TypeArgumentNullException },
                { () => services.AddAIToolbox(_ => { }, configuration, null!), "sectionName", TypeArgumentNullException },
                { () => services.AddAIToolbox(_ => { }, configuration, ""), "sectionName", TypeArgumentException },
                { () => services.AddAIToolbox(_ => { }, configuration, " "), "sectionName", TypeArgumentException }
            };
        }
    }

    [Theory]
    [MemberData(nameof(AddAIToolboxWithNullServices))]
    public void Should_Throw_On_Add_AIToolbox_With_Null_Services(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    [Theory]
    [MemberData(nameof(AddAIToolboxWithNullParameters))]
    public void Should_Throw_On_Add_AIToolbox_With_Null_Parameters(Action act, string parameterName, Type exceptionType)
    {
        // Assert
        if (exceptionType == TypeArgumentException)
        {
            act.Should().Throw<ArgumentException>().WithParameterName(parameterName);
        }
        else if (exceptionType == TypeArgumentNullException)
        {
            act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
        }
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Default_Options()
    {
        // Act
        _services.AddAIToolbox(_builderActionMock.Object);

        // Assert
        _builderActionMock.Verify(action => action(It.IsAny<AIToolboxBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Options_Action()
    {
        // Act
        _services.AddAIToolbox(_builderActionMock.Object, _optionsActionMock.Object);

        // Assert
        _builderActionMock.Verify(action => action(It.IsAny<AIToolboxBuilder>()), Times.Once);
        _optionsActionMock.Verify(action => action(It.IsAny<AIToolboxOptions>()), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Configuration_And_Default_Section_Name()
    {
        // Arrange
        var configuration = GetConfiguration("AIToolbox");

        // Act
        _services.AddAIToolbox(_builderActionMock.Object, configuration);

        // Assert
        _builderActionMock.Verify(action => action(It.IsAny<AIToolboxBuilder>()), Times.Once);
        _builderActionMock.Verify(action => action(It.Is<AIToolboxBuilder>(builder => builder.Options != null)), Times.Once);
    }

    [Fact]
    public void Should_Add_AIToolbox_With_Configuration_And_Custom_Section_Name()
    {
        // Arrange
        const string sectionName = "ConfigSection";
        var configuration = GetConfiguration(sectionName);

        // Act
        _services.AddAIToolbox(_builderActionMock.Object, configuration, sectionName);

        // Assert
        _builderActionMock.Verify(action => action(It.IsAny<AIToolboxBuilder>()), Times.Once);
        _builderActionMock.Verify(action => action(It.Is<AIToolboxBuilder>(builder => builder.Options != null)), Times.Once);
    }

    private static IConfiguration GetConfiguration(string section) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{section}:Options"] = null
            }!)
            .Build();
}
