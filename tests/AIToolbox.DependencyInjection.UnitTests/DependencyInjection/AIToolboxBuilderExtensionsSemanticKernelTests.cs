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
        var builderFactoryMock = new Mock<IBuilderFactory>();
        builderFactoryMock.SetupGet(builderFactory => builderFactory.Options).Returns(new AIToolboxOptions());
        builderFactoryMock.SetupGet(builderFactory => builderFactory.Services).Returns(new ServiceCollection());

        BuilderMock.SetupGet(builder => builder.BuilderFactory).Returns(builderFactoryMock.Object);

        // Act
        BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object);

        // Assert
        builderFactoryMock.Object.Options.SemanticKernel.Should().NotBeNull();

        BuilderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Use_SemanticKernel_With_Custom_Options()
    {
        // Arrange
        var builderFactoryMock = new Mock<IBuilderFactory>();
        builderFactoryMock.SetupGet(builderFactory => builderFactory.Options).Returns(new AIToolboxOptions());
        builderFactoryMock.SetupGet(builderFactory => builderFactory.Services).Returns(new ServiceCollection());

        BuilderMock.SetupGet(builder => builder.BuilderFactory).Returns(builderFactoryMock.Object);

        // Act
        BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object, Options);

        // Assert
        builderFactoryMock.Object.Options.SemanticKernel.Should().Be(Options);

        BuilderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Use_SemanticKernel_With_Options_Action()
    {
        // Arrange
        var builderFactoryMock = new Mock<IBuilderFactory>();
        builderFactoryMock.SetupGet(builderFactory => builderFactory.Options).Returns(new AIToolboxOptions());
        builderFactoryMock.SetupGet(builderFactory => builderFactory.Services).Returns(new ServiceCollection());

        BuilderMock.SetupGet(x => x.BuilderFactory).Returns(builderFactoryMock.Object);

        // Act
        BuilderMock.Object.UseSemanticKernel(BuilderActionMock.Object, OptionsActionMock.Object);

        // Assert
        builderFactoryMock.Object.Options.SemanticKernel.Should().NotBeNull();

        BuilderActionMock.Verify(action => action(It.IsAny<ISemanticKernelBuilder>()), Times.Once);
        OptionsActionMock.Verify(action => action(It.IsAny<SemanticKernelOptions>()), Times.Once);
    }
}
