using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeSqlServerMemoryStore(
        this IMemoryServiceBuilder builder,
        SqlServerMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.SqlServer = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.SqlServer,
            nameof(MemoryStoreOptions.SqlServer),
            $"No '{nameof(SqlServerMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, SqlServerMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeSqlServerMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<SqlServerMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new SqlServerMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeSqlServerMemoryStore(options);
    }
}
