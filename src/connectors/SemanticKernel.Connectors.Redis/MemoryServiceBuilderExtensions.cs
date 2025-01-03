using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryServiceBuilder IncludeRedisMemoryStore(
        this IMemoryServiceBuilder builder,
        RedisMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Redis = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Redis,
            nameof(MemoryStoreOptions.Redis),
            $"No '{nameof(RedisMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, RedisMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryServiceBuilder IncludeRedisMemoryStore(
        this IMemoryServiceBuilder builder,
        Action<RedisMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new RedisMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeRedisMemoryStore(options);
    }
}
