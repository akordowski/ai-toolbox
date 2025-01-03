using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class PostgresMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_PostgresMemoryStore_With_Options()
    {
        // Arrange
        var options = new PostgresMemoryStoreOptions();

        // Act
        var result = Builder.IncludePostgresMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Postgres.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_PostgresMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Postgres = new PostgresMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludePostgresMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Postgres.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_PostgresMemoryStore_With_Options_Action()
    {
        // Act
        const string connectionString = "ConnectionString";
        var result = Builder.IncludePostgresMemoryStore(options => options.ConnectionString = connectionString);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Postgres!.ConnectionString.Should().Be(connectionString);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_PostgresMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludePostgresMemoryStore((Action<PostgresMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(PostgresMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
