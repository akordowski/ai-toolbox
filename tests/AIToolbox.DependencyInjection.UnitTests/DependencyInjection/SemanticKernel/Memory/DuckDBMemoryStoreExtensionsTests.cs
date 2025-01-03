using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class DuckDBMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_DuckDBMemoryStore_With_Options()
    {
        // Arrange
        var options = new DuckDBMemoryStoreOptions();

        // Act
        var result = Builder.IncludeDuckDBMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.DuckDB.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_DuckDBMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            DuckDB = new DuckDBMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeDuckDBMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.DuckDB.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_DuckDBMemoryStore_With_Options_Action()
    {
        // Act
        const string filename = "Filename";
        var result = Builder.IncludeDuckDBMemoryStore(options => options.Filename = filename);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.DuckDB!.Filename.Should().Be(filename);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_DuckDBMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeDuckDBMemoryStore((Action<DuckDBMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(DuckDBMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
