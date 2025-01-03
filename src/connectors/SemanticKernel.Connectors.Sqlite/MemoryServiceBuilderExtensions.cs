using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeSqliteMemoryStore(
        this IMemoryServiceBuilder builder,
        SqliteMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Sqlite = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Sqlite,
            nameof(MemoryStoreOptions.Sqlite),
            $"No '{nameof(SqliteMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, SqliteMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeSqliteMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<SqliteMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new SqliteMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeSqliteMemoryStore(options);
    }
}
