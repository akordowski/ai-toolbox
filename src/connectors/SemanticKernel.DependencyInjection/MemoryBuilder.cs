using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class MemoryBuilder : IMemoryBuilder
{
    private readonly MemoryOptions _options;
    private readonly IServiceCollection _services;

    public MemoryBuilder(
        MemoryOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(MemoryOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;

        _services
            .AddSingleton(options)
            .AddSingleton<IMemoryProvider, MemoryProvider>();
    }

    public IMemoryBuilder WithSimpleMemoryStore(SimpleMemoryStoreOptions? options = null)
    {
        if (options is not null)
        {
            _options.Store ??= new MemoryStoreOptions();
            _options.Store.SimpleMemoryStore = options;
        }

        _services.AddSingleton(_options.Store?.SimpleMemoryStore!);
        _services.AddSingleton<IMemoryStoreFactory, SimpleMemoryStoreFactory>();

        return this;
    }

    public IMemoryBuilder WithSimpleMemoryStore(Action<SimpleMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Store ??= new MemoryStoreOptions();
        _options.Store.SimpleMemoryStore ??= new SimpleMemoryStoreOptions();

        optionsAction(_options.Store.SimpleMemoryStore);

        _services.AddSingleton(_options.Store.SimpleMemoryStore);
        _services.AddSingleton<IMemoryStoreFactory, SimpleMemoryStoreFactory>();

        return this;
    }
}
