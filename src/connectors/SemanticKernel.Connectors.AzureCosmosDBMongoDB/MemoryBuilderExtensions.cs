using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithAzureCosmosDBMongoDBMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.AzureCosmosDBMongoDB);

        builder.Services.AddSingleton(builder.Options.Store!.AzureCosmosDBMongoDB!);

        return builder;
    }

    public static IMemoryBuilder WithAzureCosmosDBMongoDBMemoryStore(
        this IMemoryBuilder builder,
        Action<AzureCosmosDBMongoDBMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.AzureCosmosDBMongoDB ??= new AzureCosmosDBMongoDBMemoryStoreOptions();

        optionsAction(builder.Options.Store.AzureCosmosDBMongoDB);

        return builder.WithAzureCosmosDBMongoDBMemoryStore();
    }
}
