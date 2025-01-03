using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeMongoDBMemoryStore(
        this IMemoryServiceBuilder builder,
        MongoDBMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.MongoDB = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.MongoDB,
            nameof(MemoryStoreOptions.MongoDB),
            $"No '{nameof(MongoDBMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, MongoDBMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeMongoDBMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<MongoDBMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new MongoDBMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeMongoDBMemoryStore(options);
    }
}
