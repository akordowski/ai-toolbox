using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class KustoMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_KustoMemoryStore_With_Options()
    {
        // Arrange
        var options = new KustoMemoryStoreOptions();

        // Act
        var result = Builder.IncludeKustoMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Kusto.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_KustoMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            Kusto = new KustoMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeKustoMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.Kusto.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_KustoMemoryStore_With_Options_Action()
    {
        // Act
        const string database = "Database";
        var result = Builder.IncludeKustoMemoryStore(options => options.Database = database);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.Kusto!.Database.Should().Be(database);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_KustoMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeKustoMemoryStore((Action<KustoMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(KustoMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
