using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Moq;

namespace AIToolbox.DependencyInjection;

public class KernelBuilderTests
{
    private readonly KernelOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly Mock<IBuilderService> _builderServiceMock = new();
    private readonly KernelBuilder _builder;

    public KernelBuilderTests()
    {
        _builder = new KernelBuilder(_options, _services, _builderServiceMock.Object);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var builder = new KernelBuilder(_options, services, _builderServiceMock.Object);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(KernelOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelProvider) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new KernelBuilder(null!, _services, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("No 'KernelOptions' provided. *options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new KernelBuilder(_options, null!, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_BuilderService()
    {
        // Act
        var act = () => new KernelBuilder(_options, _services, null!);

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
            .Returns(Mock.Of<IAgentsBuilder>());

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
            .Returns(Mock.Of<IAgentsBuilder>());

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
            .Returns(Mock.Of<IMemoryBuilder>());

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
            .Returns(Mock.Of<IMemoryBuilder>());

        // Act
        var result = _builder.AddMemory(optionsAction);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddMemory(optionsAction), Times.Once);
    }

    [Fact]
    public void Should_Add_Custom_AIServiceSelector()
    {
        // Act
        var result = _builder.WithCustomAIServiceSelector(_ => Mock.Of<IAIServiceSelector>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Add_Custom_AIServiceSelector_As_Instance()
    {
        // Act
        var result = _builder.WithCustomAIServiceSelector(Mock.Of<IAIServiceSelector>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Add_Custom_FunctionInvocationFilter()
    {
        // Act
        var result = _builder.WithCustomFunctionInvocationFilter(_ => Mock.Of<IFunctionInvocationFilter>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Add_Custom_FunctionInvocationFilter_As_Instance()
    {
        // Act
        var result = _builder.WithCustomFunctionInvocationFilter(Mock.Of<IFunctionInvocationFilter>());

        // Assert
        result.Should().Be(_builder);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IKernelBuilderConfigurator) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
