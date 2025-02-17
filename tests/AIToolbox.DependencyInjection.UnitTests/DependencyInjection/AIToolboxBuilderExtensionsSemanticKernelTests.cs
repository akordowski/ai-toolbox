using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class AIToolboxBuilderExtensionsSemanticKernelTests
{
    private static readonly IAIToolboxBuilder NullBuilder = null!;
    private static readonly SemanticKernelOptions Options = new();
    private static readonly Mock<IAIToolboxBuilder> BuilderMock = new();
    private static readonly Action<ISemanticKernelBuilder> NullBuilderAction = null!;
    private static readonly Mock<Action<ISemanticKernelBuilder>> BuilderActionMock = new();
    private static readonly Mock<Action<SemanticKernelOptions>> OptionsActionMock = new();

    public AIToolboxBuilderExtensionsSemanticKernelTests()
    {
        BuilderMock.Reset();
        BuilderActionMock.Reset();
        OptionsActionMock.Reset();
    }

    public static TheoryData<Action> UseSemanticKernelWithNullBuilder =>
    [
        () => NullBuilder.UseSemanticKernel(BuilderActionMock.Object),
        () => NullBuilder.UseSemanticKernel(BuilderActionMock.Object, Options),
        () => NullBuilder.UseSemanticKernel(BuilderActionMock.Object, OptionsActionMock.Object)
    ];

    public static TheoryData<Action, string> UseSemanticKernelWithParameters =>
        new()
        {
            { () => BuilderMock.Object.UseSemanticKernel(NullBuilderAction), "builderAction" },

            { () => BuilderMock.Object.UseSemanticKernel(NullBuilderAction, Options), "builderAction" },
            { () => BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object, (SemanticKernelOptions)null!), "options" },

            { () => BuilderMock.Object.UseSemanticKernel(NullBuilderAction, OptionsActionMock.Object), "builderAction" },
            { () => BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object, (Action<SemanticKernelOptions>)null!), "optionsAction" }
        };

    [Theory]
    [MemberData(nameof(UseSemanticKernelWithNullBuilder))]
    public void Should_Throw_On_Use_SemanticKernel_When_Builder_Is_Null(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("builder");
    }

    [Theory]
    [MemberData(nameof(UseSemanticKernelWithParameters))]
    public void Should_Throw_On_Use_SemanticKernel_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Use_SemanticKernel_With_Default_Options()
    {
        // Arrange
        BuilderMock.SetupGet(builder => builder.Options).Returns(new AIToolboxOptions());
        BuilderMock.SetupGet(builder => builder.Services).Returns(new ServiceCollection());

        // Act
        BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object);

        // Assert
        BuilderMock.Object.Options.SemanticKernel.Should().NotBeNull();

        BuilderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Use_SemanticKernel_With_Custom_Options()
    {
        // Arrange
        BuilderMock.SetupGet(builder => builder.Options).Returns(new AIToolboxOptions());
        BuilderMock.SetupGet(builder => builder.Services).Returns(new ServiceCollection());

        // Act
        BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object, Options);

        // Assert
        BuilderMock.Object.Options.SemanticKernel.Should().Be(Options);

        BuilderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Use_SemanticKernel_With_Options_Action()
    {
        // Arrange
        BuilderMock.SetupGet(builder => builder.Options).Returns(new AIToolboxOptions());
        BuilderMock.SetupGet(builder => builder.Services).Returns(new ServiceCollection());

        // Act
        BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object, OptionsActionMock.Object);

        // Assert
        BuilderMock.Object.Options.SemanticKernel.Should().NotBeNull();

        BuilderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
        OptionsActionMock.Verify(action => action(It.IsAny<SemanticKernelOptions>()), Times.Once);
    }
}
