using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithChromaMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Chroma);

        builder.Services
            .AddSingleton(builder.Options.Store!.Chroma!)
            .AddSingleton<IMemoryStoreFactory, ChromaMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder WithChromaMemoryStore(
        this IMemoryBuilder builder,
        Action<ChromaMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Chroma ??= new ChromaMemoryStoreOptions();

        optionsAction(builder.Options.Store.Chroma);

        return builder.WithChromaMemoryStore();
    }
}
