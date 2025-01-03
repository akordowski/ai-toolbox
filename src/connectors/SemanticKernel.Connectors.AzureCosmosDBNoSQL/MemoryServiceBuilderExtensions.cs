using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeAzureCosmosDBNoSQLMemoryStore(
        this IMemoryServiceBuilder builder,
        AzureCosmosDBNoSQLMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.AzureCosmosDBNoSQL = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.AzureCosmosDBNoSQL,
            nameof(MemoryStoreOptions.AzureCosmosDBNoSQL),
            $"No '{nameof(AzureCosmosDBNoSQLMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, AzureCosmosDBNoSQLMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeAzureCosmosDBNoSQLMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<AzureCosmosDBNoSQLMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AzureCosmosDBNoSQLMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeAzureCosmosDBNoSQLMemoryStore(options);
    }
}
