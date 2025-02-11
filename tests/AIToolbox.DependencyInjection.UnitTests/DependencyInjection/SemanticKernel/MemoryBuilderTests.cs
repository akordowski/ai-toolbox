using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class MemoryBuilderTests
{
    private readonly MemoryOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly MemoryBuilder _builder;

    public MemoryBuilderTests()
    {
        _builder = new MemoryBuilder(_options, _services);
    }

    public static TheoryData<Action, string, string> BuilderWithInvalidParameters =>
        new()
        {
            { () => _ = new MemoryBuilder(null!, null!), "options", "No 'MemoryOptions' provided.*" },
            { () => _ = new MemoryBuilder(new MemoryOptions(), null!), "services", "*" }
        };

    public static TheoryData<Action, string> MethodsWithInvalidParameters
    {
        get
        {
            var builder = new MemoryBuilder(new MemoryOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.WithSimpleMemoryStore((Action<SimpleMemoryStoreOptions>)null!), "optionsAction" }
            };
        }
    }

    [Theory]
    [MemberData(nameof(BuilderWithInvalidParameters))]
    public void Should_Throw_On_Construct_With_Invalid_Parameters(Action act, string parameterName, string message)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(parameterName)
            .WithMessage(message);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        _ = new MemoryBuilder(_options, services);

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(MemoryOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryProvider) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Theory]
    [MemberData(nameof(MethodsWithInvalidParameters))]
    public void Should_Throw_On_Methods_With_Invalid_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Add_SimpleMemoryStore_With_Default_Options()
    {
        // Arrange
        _options.Store = new MemoryStoreOptions
        {
            SimpleMemoryStore = new SimpleMemoryStoreOptions()
        };

        // Act
        var result = _builder.WithSimpleMemoryStore();

        // Assert
        result.Should().Be(_builder);
        _options.Store.SimpleMemoryStore.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Add_SimpleMemoryStore_With_Custom_Options()
    {
        // Arrange
        var options = new SimpleMemoryStoreOptions();

        // Act
        var result = _builder.WithSimpleMemoryStore(options);

        // Assert
        result.Should().Be(_builder);
        _options.Store!.SimpleMemoryStore.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Add_SimpleMemoryStore_With_Options_Action()
    {
        // Act
        var result = _builder.WithSimpleMemoryStore(options => options.StorageType = StorageType.Volatile);

        // Assert
        result.Should().Be(_builder);
        _options.Store!.SimpleMemoryStore!.StorageType.Should().Be(StorageType.Volatile);

        AssertServices();
    }

    private void AssertServices()
    {
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(SimpleMemoryStoreOptions) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);

        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                       descriptor.ImplementationType == typeof(SimpleMemoryStoreFactory) &&
                                                       descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
