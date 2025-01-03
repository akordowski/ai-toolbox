using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeQdrantMemoryStore(
        this IMemoryServiceBuilder builder,
        QdrantMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Qdrant = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Qdrant,
            nameof(MemoryStoreOptions.Qdrant),
            $"No '{nameof(QdrantMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, QdrantMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeQdrantMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<QdrantMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new QdrantMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeQdrantMemoryStore(options);
    }
}
