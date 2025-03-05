using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithPostgresMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Postgres);

        builder.Services
            .AddSingleton(builder.Options.Store!.Postgres!)
            .AddSingleton<IMemoryStoreFactory, PostgresMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder WithPostgresMemoryStore(
        this IMemoryBuilder builder,
        Action<PostgresMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Postgres ??= new PostgresMemoryStoreOptions();

        optionsAction(builder.Options.Store.Postgres);

        return builder.WithPostgresMemoryStore();
    }
}
