using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class ConnectorServiceBuilderTests
{
    private readonly GlobalConnectorOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly Mock<IServiceBuilderService> _builderServiceMock = new();
    private readonly IKernelServiceBuilder _kernelBuilder = Mock.Of<IKernelServiceBuilder>();
    private readonly ConnectorServiceBuilder _builder;

    public ConnectorServiceBuilderTests()
    {
        _builder = new ConnectorServiceBuilder(_options, _services, _builderServiceMock.Object);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        var builder = new ConnectorServiceBuilder(_options, _services, _builderServiceMock.Object);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(_services);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new ConnectorServiceBuilder(null!, _services, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new ConnectorServiceBuilder(_options, null!, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_BuilderService()
    {
        // Act
        var act = () => new ConnectorServiceBuilder(_options, _services, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*builderService*");
    }

    [Fact]
    public void Should_Add_Kernel_With_Options()
    {
        // Arrange
        var options = new KernelOptions();
        _builderServiceMock
            .Setup(o => o.AddKernel(options))
            .Returns(_kernelBuilder);

        // Act
        var result = _builder.AddKernel(options);

        // Assert
        result.Should().Be(_kernelBuilder);
        _builderServiceMock.Verify(o => o.AddKernel(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Kernel_With_Null_Options()
    {
        // Arrange
        KernelOptions? options = null;
        _builderServiceMock
            .Setup(o => o.AddKernel(options))
            .Returns(_kernelBuilder);

        // Act
        var result = _builder.AddKernel(options);

        // Assert
        result.Should().Be(_kernelBuilder);
        _builderServiceMock.Verify(o => o.AddKernel(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Kernel_With_Options_Action()
    {
        // Arrange
        _builderServiceMock
            .Setup(o => o.AddKernel(It.IsAny<Action<KernelOptions>>()))
            .Returns(_kernelBuilder);

        // Act
        var result = _builder.AddKernel(options => options.AddLogging = true);

        // Assert
        result.Should().Be(_kernelBuilder);
        _builderServiceMock.Verify(o => o.AddKernel(It.IsAny<Action<KernelOptions>>()), Times.Once);
    }
}
