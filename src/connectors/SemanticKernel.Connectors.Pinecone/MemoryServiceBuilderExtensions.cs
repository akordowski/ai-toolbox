using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludePineconeMemoryStore(
        this IMemoryServiceBuilder builder,
        PineconeMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Pinecone = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Pinecone,
            nameof(MemoryStoreOptions.Pinecone),
            $"No '{nameof(PineconeMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, PineconeMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludePineconeMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<PineconeMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new PineconeMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludePineconeMemoryStore(options);
    }
}
