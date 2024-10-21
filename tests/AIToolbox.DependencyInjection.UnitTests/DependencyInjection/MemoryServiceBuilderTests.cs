using AIToolbox.Options;
using AIToolbox.Options.Agents;
using AIToolbox.Options.KernelMemory;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class MemoryServiceBuilderTests
{
    private readonly MemoryOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly Mock<IServiceBuilderService> _builderServiceMock = new();
    private readonly MemoryServiceBuilder _builder;

    public MemoryServiceBuilderTests()
    {
        _builder = new MemoryServiceBuilder(_options, _services, _builderServiceMock.Object);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var builder = new MemoryServiceBuilder(_options, services, _builderServiceMock.Object);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(MemoryOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryProvider) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new MemoryServiceBuilder(null!, _services, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("No 'MemoryOptions' provided. *options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new MemoryServiceBuilder(_options, null!, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_BuilderService()
    {
        // Act
        var act = () => new MemoryServiceBuilder(_options, _services, null!);

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
    public void Should_Add_KernelMemory_With_Options()
    {
        // Arrange
        var options = new KernelMemoryOptions();
        _builderServiceMock
            .Setup(o => o.AddKernelMemory(options))
            .Returns(Mock.Of<IKernelMemoryServiceBuilder>());

        // Act
        var result = _builder.AddKernelMemory(options);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddKernelMemory(options), Times.Once);
    }

    [Fact]
    public void Should_Add_KernelMemory_With_Options_Action()
    {
        // Arrange
        Action<KernelMemoryOptions> optionsAction = _ => { };
        _builderServiceMock
            .Setup(o => o.AddKernelMemory(optionsAction))
            .Returns(Mock.Of<IKernelMemoryServiceBuilder>());

        // Act
        var result = _builder.AddKernelMemory(optionsAction);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddKernelMemory(optionsAction), Times.Once);
    }

    [Fact]
    public void Should_Include_SimpleMemoryStore_With_Options()
    {
        // Arrange
        var options = new SimpleMemoryStoreOptions();

        // Act
        var result = _builder.IncludeSimpleMemoryStore(options);

        // Assert
        result.Should().Be(_builder);
        _options.Store!.SimpleMemoryStore.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_SimpleMemoryStore_With_Null_Options()
    {
        // Arrange
        _options.Store = new MemoryStoreOptions
        {
            SimpleMemoryStore = new SimpleMemoryStoreOptions()
        };

        // Act
        var result = _builder.IncludeSimpleMemoryStore();

        // Assert
        result.Should().Be(_builder);
        _options.Store.SimpleMemoryStore.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_SimpleMemoryStore_With_Options_Action()
    {
        // Act
        var result = _builder.IncludeSimpleMemoryStore(options => options.StorageType = StorageType.Volatile);

        // Assert
        result.Should().Be(_builder);
        _options.Store!.SimpleMemoryStore!.StorageType.Should().Be(StorageType.Volatile);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_SimpleMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => _builder.IncludeSimpleMemoryStore((Action<SimpleMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                       descriptor.ImplementationType == typeof(SimpleMemoryStoreFactory) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
