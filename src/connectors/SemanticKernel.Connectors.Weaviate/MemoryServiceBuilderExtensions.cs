using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeWeaviateMemoryStore(
        this IMemoryServiceBuilder builder,
        WeaviateMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Weaviate = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Weaviate,
            nameof(MemoryStoreOptions.Weaviate),
            $"No '{nameof(WeaviateMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, WeaviateMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeWeaviateMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<WeaviateMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new WeaviateMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeWeaviateMemoryStore(options);
    }
}
