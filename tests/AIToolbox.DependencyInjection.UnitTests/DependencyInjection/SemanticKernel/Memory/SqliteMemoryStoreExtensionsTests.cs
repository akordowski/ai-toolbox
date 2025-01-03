using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SqliteMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_SqliteMemoryStore_With_Options()
    {
        // Arrange
        var options = new SqliteMemoryStoreOptions();

        // Act
        var result = Builder.IncludeSqliteMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Sqlite.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_SqliteMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Sqlite = new SqliteMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeSqliteMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Sqlite.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_SqliteMemoryStore_With_Options_Action()
    {
        // Act
        const string filename = "Filename";
        var result = Builder.IncludeSqliteMemoryStore(options => options.Filename = filename);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Sqlite!.Filename.Should().Be(filename);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_SqliteMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeSqliteMemoryStore((Action<SqliteMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(SqliteMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
