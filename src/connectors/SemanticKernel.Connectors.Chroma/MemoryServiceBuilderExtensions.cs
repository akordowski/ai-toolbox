using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeChromaMemoryStore(
        this IMemoryServiceBuilder builder,
        ChromaMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Chroma = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Chroma,
            nameof(MemoryStoreOptions.Chroma),
            $"No '{nameof(ChromaMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, ChromaMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeChromaMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<ChromaMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new ChromaMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeChromaMemoryStore(options);
    }
}
