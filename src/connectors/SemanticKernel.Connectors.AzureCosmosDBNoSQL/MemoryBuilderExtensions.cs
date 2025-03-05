using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithAzureCosmosDBNoSQLMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.AzureCosmosDBNoSQL);

        builder.Services.AddSingleton(builder.Options.Store!.AzureCosmosDBNoSQL!);

        return builder;
    }

    public static IMemoryBuilder WithAzureCosmosDBNoSQLMemoryStore(
        this IMemoryBuilder builder,
        Action<AzureCosmosDBNoSQLMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.AzureCosmosDBNoSQL ??= new AzureCosmosDBNoSQLMemoryStoreOptions();

        optionsAction(builder.Options.Store.AzureCosmosDBNoSQL);

        return builder.WithAzureCosmosDBNoSQLMemoryStore();
    }
}
