using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithAzureAISearchMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.AzureAISearch);

        builder.Services
            .AddSingleton(builder.Options.Store!.AzureAISearch!)
            .AddSingleton<IMemoryStoreFactory, AzureAISearchMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder WithAzureAISearchMemoryStore(
        this IMemoryBuilder builder,
        Action<AzureAISearchMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.AzureAISearch ??= new AzureAISearchMemoryStoreOptions();

        optionsAction(builder.Options.Store.AzureAISearch);

        return builder.WithAzureAISearchMemoryStore();
    }
}
