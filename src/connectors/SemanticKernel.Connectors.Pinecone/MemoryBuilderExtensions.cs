using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithPineconeMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Pinecone);

        builder.Services.AddSingleton(builder.Options.Store!.Pinecone!);

        return builder;
    }

    public static IMemoryBuilder WithPineconeMemoryStore(
        this IMemoryBuilder builder,
        Action<PineconeMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Pinecone ??= new PineconeMemoryStoreOptions();

        optionsAction(builder.Options.Store.Pinecone);

        return builder.WithPineconeMemoryStore();
    }
}
