using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithMilvusMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Milvus);

        builder.Services.AddSingleton(builder.Options.Store!.Milvus!);

        return builder;
    }

    public static IMemoryBuilder WithMilvusMemoryStore(
        this IMemoryBuilder builder,
        Action<MilvusMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Milvus ??= new MilvusMemoryStoreOptions();

        optionsAction(builder.Options.Store.Milvus);

        return builder.WithMilvusMemoryStore();
    }
}
