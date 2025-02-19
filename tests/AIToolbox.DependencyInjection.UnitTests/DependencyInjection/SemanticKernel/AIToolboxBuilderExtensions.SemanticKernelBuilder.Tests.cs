using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class AIToolboxBuilderExtensionsSemanticKernelBuilderTests
{
    private readonly Mock<Action<ISemanticKernelBuilder>> _builderActionMock = new();
    private readonly Mock<Action<SemanticKernelOptions>> _optionsActionMock = new();

    public static TheoryData<Action> AddSemanticKernelWithNullBuilder
    {
        get
        {
            IAIToolboxBuilder builder = null!;

            return
            [
                () => builder.AddSemanticKernel(_ => { }),
                () => builder.AddSemanticKernel(_ => { }, _ => { })
            ];
        }
    }

    public static TheoryData<Action, string> AddSemanticKernelWithInvalidParameters
    {
        get
        {
            Action<ISemanticKernelBuilder> nullBuilderAction = null!;
            Action<SemanticKernelOptions> nullOptionsAction = null!;

            var builder = new AIToolboxBuilder(new AIToolboxOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.AddSemanticKernel(nullBuilderAction), "builderAction" },

                { () => builder.AddSemanticKernel(nullBuilderAction, _ => { }), "builderAction" },
                { () => builder.AddSemanticKernel(_ => { }, nullOptionsAction), "optionsAction" }
            };
        }
    }

    [Theory]
    [MemberData(nameof(AddSemanticKernelWithNullBuilder))]
    public void Should_Throw_On_Add_SemanticKernel_With_Null_Builder(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builder");
    }

    [Theory]
    [MemberData(nameof(AddSemanticKernelWithInvalidParameters))]
    public void Should_Throw_On_Add_SemanticKernel_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Add_SemanticKernel_With_Default_Options()
    {
        // Arrange
        var options = new AIToolboxOptions();
        var builder = CreateBuilder(options);

        // Act
        builder.AddSemanticKernel(_builderActionMock.Object);

        // Assert
        options.SemanticKernel.Should().NotBeNull();

        _builderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Add_SemanticKernel_With_Options_Action()
    {
        // Arrange
        var options = new AIToolboxOptions();
        var builder = CreateBuilder(options);

        // Act
        builder.AddSemanticKernel(_builderActionMock.Object, _optionsActionMock.Object);

        // Assert
        options.SemanticKernel.Should().NotBeNull();

        _builderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
        _optionsActionMock.Verify(action => action(It.IsAny<SemanticKernelOptions>()), Times.Once);
    }

    private static AIToolboxBuilder CreateBuilder(AIToolboxOptions options) =>
        new(options, new ServiceCollection());
}
