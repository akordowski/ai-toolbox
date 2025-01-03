using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class RedisMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_RedisMemoryStore_With_Options()
    {
        // Arrange
        var options = new RedisMemoryStoreOptions();

        // Act
        var result = Builder.IncludeRedisMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Redis.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_RedisMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Redis = new RedisMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeRedisMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Redis.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_RedisMemoryStore_With_Options_Action()
    {
        // Act
        const string connectionString = "ConnectionString";
        var result = Builder.IncludeRedisMemoryStore(options => options.ConnectionString = connectionString);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Redis!.ConnectionString.Should().Be(connectionString);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_RedisMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeRedisMemoryStore((Action<RedisMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(RedisMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
