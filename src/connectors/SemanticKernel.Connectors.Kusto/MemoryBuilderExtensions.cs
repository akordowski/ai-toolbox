using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithKustoMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Kusto);

        builder.Services.AddSingleton(builder.Options.Store!.Kusto!);

        return builder;
    }

    public static IMemoryBuilder WithKustoMemoryStore(
        this IMemoryBuilder builder,
        Action<KustoMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Kusto ??= new KustoMemoryStoreOptions();

        optionsAction(builder.Options.Store.Kusto);

        return builder.WithKustoMemoryStore();
    }
}
