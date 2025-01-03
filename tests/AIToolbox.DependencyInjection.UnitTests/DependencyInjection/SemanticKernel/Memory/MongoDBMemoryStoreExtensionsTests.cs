using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class MongoDBMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_MongoDBMemoryStore_With_Options()
    {
        // Arrange
        var options = new MongoDBMemoryStoreOptions();

        // Act
        var result = Builder.IncludeMongoDBMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.MongoDB.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_MongoDBMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            MongoDB = new MongoDBMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeMongoDBMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.MongoDB.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_MongoDBMemoryStore_With_Options_Action()
    {
        // Act
        const string connectionString = "ConnectionString";
        var result = Builder.IncludeMongoDBMemoryStore(options => options.ConnectionString = connectionString);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.MongoDB!.ConnectionString.Should().Be(connectionString);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_MongoDBMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeMongoDBMemoryStore((Action<MongoDBMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(MongoDBMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
