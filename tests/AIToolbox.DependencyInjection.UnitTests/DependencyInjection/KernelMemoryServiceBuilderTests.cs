using AIToolbox.KernelMemory;
using AIToolbox.Options.Agents;
using AIToolbox.Options.KernelMemory;
using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class KernelMemoryServiceBuilderTests
{
    private readonly KernelMemoryOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly Mock<IServiceBuilderService> _builderServiceMock = new();
    private readonly KernelMemoryServiceBuilder _builder;

    public KernelMemoryServiceBuilderTests()
    {
        _builder = new KernelMemoryServiceBuilder(_options, _services, _builderServiceMock.Object);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var builder = new KernelMemoryServiceBuilder(_options, services, _builderServiceMock.Object);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(KernelMemoryOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelMemoryProvider) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new KernelMemoryServiceBuilder(null!, _services, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("No 'KernelMemoryOptions' provided. *options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new KernelMemoryServiceBuilder(_options, null!, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_BuilderService()
    {
        // Act
        var act = () => new KernelMemoryServiceBuilder(_options, _services, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*builderService*");
    }

    [Fact]
    public void Should_Add_Agents_With_Options()
    {
        // Arrange
        var options = new AgentOptions();
        _builderServiceMock
            .Setup(o => o.AddAgents(options))
            .Returns(Mock.Of<IAgentServiceBuilder>());

        // Act
        var result = _builder.AddAgents(options);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddAgents(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Agents_With_Options_Action()
    {
        // Arrange
        Action<AgentOptions> optionsAction = _ => { };
        _builderServiceMock
            .Setup(o => o.AddAgents(optionsAction))
            .Returns(Mock.Of<IAgentServiceBuilder>());

        // Act
        var result = _builder.AddAgents(optionsAction);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddAgents(optionsAction), Times.Once);
    }

    [Fact]
    public void Should_Add_Memory_With_Options()
    {
        // Arrange
        var options = new MemoryOptions();
        _builderServiceMock
            .Setup(o => o.AddMemory(options))
            .Returns(Mock.Of<IMemoryServiceBuilder>());

        // Act
        var result = _builder.AddMemory(options);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddMemory(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Memory_With_Options_Action()
    {
        // Arrange
        Action<MemoryOptions> optionsAction = _ => { };
        _builderServiceMock
            .Setup(o => o.AddMemory(optionsAction))
            .Returns(Mock.Of<IMemoryServiceBuilder>());

        // Act
        var result = _builder.AddMemory(optionsAction);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddMemory(optionsAction), Times.Once);
    }
}
