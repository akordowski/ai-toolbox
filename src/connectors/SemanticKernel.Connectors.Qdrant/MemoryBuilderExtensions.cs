using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithQdrantMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Qdrant);

        builder.Services
            .AddSingleton(builder.Options.Store!.Qdrant!)
            .AddSingleton<IMemoryStoreFactory, QdrantMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder WithQdrantMemoryStore(
        this IMemoryBuilder builder,
        Action<QdrantMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Qdrant ??= new QdrantMemoryStoreOptions();

        optionsAction(builder.Options.Store.Qdrant);

        return builder.WithQdrantMemoryStore();
    }
}
