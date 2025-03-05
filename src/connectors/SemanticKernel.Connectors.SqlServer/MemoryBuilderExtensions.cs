using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithSqlServerMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.SqlServer);

        builder.Services.AddSingleton(builder.Options.Store!.SqlServer!);

        return builder;
    }

    public static IMemoryBuilder WithSqlServerMemoryStore(
        this IMemoryBuilder builder,
        Action<SqlServerMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.SqlServer ??= new SqlServerMemoryStoreOptions();

        optionsAction(builder.Options.Store.SqlServer);

        return builder.WithSqlServerMemoryStore();
    }
}
