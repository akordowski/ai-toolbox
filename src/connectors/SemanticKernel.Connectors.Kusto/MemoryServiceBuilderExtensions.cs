using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeKustoMemoryStore(
        this IMemoryServiceBuilder builder,
        KustoMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Kusto = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Kusto,
            nameof(MemoryStoreOptions.Kusto),
            $"No '{nameof(KustoMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, KustoMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeKustoMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<KustoMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new KustoMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeKustoMemoryStore(options);
    }
}
