using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeAzureCosmosDBMongoDBMemoryStore(
        this IMemoryServiceBuilder builder,
        AzureCosmosDBMongoDBMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.AzureCosmosDBMongoDB = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.AzureCosmosDBMongoDB,
            nameof(MemoryStoreOptions.AzureCosmosDBMongoDB),
            $"No '{nameof(AzureCosmosDBMongoDBMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, AzureCosmosDBMongoDBMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeAzureCosmosDBMongoDBMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<AzureCosmosDBMongoDBMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AzureCosmosDBMongoDBMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeAzureCosmosDBMongoDBMemoryStore(options);
    }
}
