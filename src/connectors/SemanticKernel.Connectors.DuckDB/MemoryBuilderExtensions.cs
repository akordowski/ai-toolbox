using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithDuckDBMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.DuckDB);

        builder.Services.AddSingleton(builder.Options.Store!.DuckDB!);

        return builder;
    }

    public static IMemoryBuilder WithDuckDBMemoryStore(
        this IMemoryBuilder builder,
        Action<DuckDBMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.DuckDB ??= new DuckDBMemoryStoreOptions();

        optionsAction(builder.Options.Store.DuckDB);

        return builder.WithDuckDBMemoryStore();
    }
}
