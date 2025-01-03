using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeAzureAISearchMemoryStore(
        this IMemoryServiceBuilder builder,
        AzureAISearchMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.AzureAISearch = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.AzureAISearch,
            nameof(MemoryStoreOptions.AzureAISearch),
            $"No '{nameof(AzureAISearchMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, AzureAISearchMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeAzureAISearchMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<AzureAISearchMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AzureAISearchMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeAzureAISearchMemoryStore(options);
    }
}
