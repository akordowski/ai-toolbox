using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SqlServerMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_SqlServerMemoryStore_With_Options()
    {
        // Arrange
        var options = new SqlServerMemoryStoreOptions();

        // Act
        var result = Builder.IncludeSqlServerMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.SqlServer.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_SqlServerMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            SqlServer = new SqlServerMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeSqlServerMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.SqlServer.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_SqlServerMemoryStore_With_Options_Action()
    {
        // Act
        const string connectionString = "ConnectionString";
        var result = Builder.IncludeSqlServerMemoryStore(options => options.ConnectionString = connectionString);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.SqlServer!.ConnectionString.Should().Be(connectionString);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_SqlServerMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeSqlServerMemoryStore((Action<SqlServerMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(SqlServerMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
