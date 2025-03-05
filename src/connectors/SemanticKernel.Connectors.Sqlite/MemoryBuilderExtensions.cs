using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithSqliteMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Sqlite);

        builder.Services.AddSingleton(builder.Options.Store!.Sqlite!);

        return builder;
    }

    public static IMemoryBuilder WithSqliteMemoryStore(
        this IMemoryBuilder builder,
        Action<SqliteMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Sqlite ??= new SqliteMemoryStoreOptions();

        optionsAction(builder.Options.Store.Sqlite);

        return builder.WithSqliteMemoryStore();
    }
}
