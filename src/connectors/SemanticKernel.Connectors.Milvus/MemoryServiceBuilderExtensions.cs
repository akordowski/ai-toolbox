using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeMilvusMemoryStore(
        this IMemoryServiceBuilder builder,
        MilvusMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Milvus = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Milvus,
            nameof(MemoryStoreOptions.Milvus),
            $"No '{nameof(MilvusMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, MilvusMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeMilvusMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<MilvusMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new MilvusMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeMilvusMemoryStore(options);
    }
}
