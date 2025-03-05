using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryBuilderExtensions
{
    public static IMemoryBuilder WithRedisMemoryStore(this IMemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Store?.Redis);

        builder.Services
            .AddSingleton(builder.Options.Store!.Redis!)
            .AddSingleton<IMemoryStoreFactory, RedisMemoryStoreFactory>();

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
