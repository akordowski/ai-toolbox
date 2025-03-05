using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithRedisMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Redis);

        builder.Services.AddSingleton(builder.Options.Store!.Redis!);

        return builder;
    }

    public static IMemoryBuilder WithRedisMemoryStore(
        this IMemoryBuilder builder,
        Action<RedisMemoryStoreOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Store ??= new MemoryStoreOptions();
        builder.Options.Store.Redis ??= new RedisMemoryStoreOptions();

        optionsAction(builder.Options.Store.Redis);

        return builder.WithRedisMemoryStore();
    }
}
