using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithWeaviateMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Weaviate);

        builder.Services
            .AddSingleton(builder.Options.Store!.Weaviate!)
            .AddSingleton<IMemoryStoreFactory, WeaviateMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder WithWeaviateMemoryStore(
        this IMemoryBuilder builder,
        Action<WeaviateMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Weaviate ??= new WeaviateMemoryStoreOptions();

        optionsAction(builder.Options.Store.Weaviate);

        return builder.WithWeaviateMemoryStore();
    }
}
