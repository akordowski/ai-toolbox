using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTests
{
    private readonly SemanticKernelOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly SemanticKernelBuilder _builder;

    public SemanticKernelBuilderTests()
    {
        _builder = new SemanticKernelBuilder(_options, _services);
    }

    public static TheoryData<Action, string> ConstructWithNullParameters =>
        new()
        {
            { () => _ = new SemanticKernelBuilder(null!, null!), "options" },
            { () => _ = new SemanticKernelBuilder(new SemanticKernelOptions(), null!), "services" }
        };

    public static TheoryData<Action, string> AddWithNullParameters
    {
        get
        {
            var builder = new SemanticKernelBuilder(new SemanticKernelOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.AddKernel(null!), "builderAction" },
                { () => builder.AddKernel(null!, _ => { }), "builderAction" },
                { () => builder.AddKernel(_ => { }, null!), "optionsAction" }
            };
        }
    }

    public static TheoryData<Action, string> AddWithoutDefaultOptions
    {
        get
        {
            var builder = new SemanticKernelBuilder(new SemanticKernelOptions(), new ServiceCollection());
            const string kernelOptionsMessage = "No 'KernelOptions' provided.*";

            return new TheoryData<Action, string>
            {
                { () => builder.AddKernel(), kernelOptionsMessage }
            };
        }
    }

    [Theory]
    [MemberData(nameof(ConstructWithNullParameters))]
    public void Should_Throw_On_Construct_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        _ = new SemanticKernelBuilder(new SemanticKernelOptions(), new ServiceCollection());
    }

    [Theory]
    [MemberData(nameof(AddWithNullParameters))]
    public void Should_Throw_On_Add_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Theory]
    [MemberData(nameof(AddWithoutDefaultOptions))]
    public void Should_Throw_On_Add_Without_Default_Options(Action act, string message)
    {
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_Kernel_With_Default_Options()
    {
        // Arrange
        var kernelOptions = new KernelOptions();
        _options.Kernel = kernelOptions;

        // Act
        var result = _builder.AddKernel();

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().Be(kernelOptions);

        AssertAddKernel(kernelOptions);
    }

    [Fact]
    public void Should_Add_Kernel_With_Builder_Action()
    {
        // Arrange
        var kernelBuilderActionMock = new Mock<Action<IKernelBuilder>>();

        // Act
        var result = _builder.AddKernel(kernelBuilderActionMock.Object);

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().NotBeNull();

        AssertAddKernel();

        kernelBuilderActionMock.Verify(action => action(It.IsAny<IKernelBuilder>()), Times.Once);
    }

    [Fact]
    public void Should_Add_Kernel_With_Builder_And_Options_Action()
    {
        // Arrange
        var kernelBuilderActionMock = new Mock<Action<IKernelBuilder>>();
        var kernelOptionsActionMock = new Mock<Action<KernelOptions>>();

        // Act
        var result = _builder.AddKernel(kernelBuilderActionMock.Object, kernelOptionsActionMock.Object);

        // Assert
        result.Should().Be(_builder);
        _options.Kernel.Should().NotBeNull();

        AssertAddKernel();

        kernelBuilderActionMock.Verify(action => action(It.IsAny<IKernelBuilder>()), Times.Once);
        kernelOptionsActionMock.Verify(action => action(It.IsAny<KernelOptions>()), Times.Once);
    }

    private void AssertAddKernel(KernelOptions? options = null)
    {
        if (options is null)
        {
            _services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                           descriptor.ServiceType == typeof(KernelOptions) &&
                                                           descriptor.ImplementationInstance != null);
        }
        else
        {
            _services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                           descriptor.ServiceType == typeof(KernelOptions) &&
                                                           descriptor.ImplementationInstance == options);
        }
    }
}
