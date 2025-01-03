using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class MilvusMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_MilvusMemoryStore_With_Options()
    {
        // Arrange
        var options = new MilvusMemoryStoreOptions();

        // Act
        var result = Builder.IncludeMilvusMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Milvus.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_MilvusMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Milvus = new MilvusMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeMilvusMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Milvus.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_MilvusMemoryStore_With_Options_Action()
    {
        // Act
        const string database = "Database";
        var result = Builder.IncludeMilvusMemoryStore(options => options.Database = database);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Milvus!.Database.Should().Be(database);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_MilvusMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeMilvusMemoryStore((Action<MilvusMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(MilvusMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
