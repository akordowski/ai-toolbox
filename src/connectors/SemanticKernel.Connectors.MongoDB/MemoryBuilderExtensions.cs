using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithMongoDBMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.MongoDB);

        builder.Services.AddSingleton(builder.Options.Store!.MongoDB!);

        return builder;
    }

    public static IMemoryBuilder WithMongoDBMemoryStore(
        this IMemoryBuilder builder,
        Action<MongoDBMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.MongoDB ??= new MongoDBMemoryStoreOptions();

        optionsAction(builder.Options.Store.MongoDB);

        return builder.WithMongoDBMemoryStore();
    }
}
