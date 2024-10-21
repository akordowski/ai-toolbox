using AIToolbox.Options.Agents;
using AIToolbox.Options.KernelMemory;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class MemoryServiceBuilder : IMemoryServiceBuilder
{
    public MemoryOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IServiceBuilderService _builderService;

    public MemoryServiceBuilder(
        MemoryOptions options,
        IServiceCollection services,
        IServiceBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(MemoryOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;

        Services
            .AddSingleton(Options)
            .AddSingleton<IMemoryProvider, MemoryProvider>();
    }

    public IAgentServiceBuilder AddAgents(AgentOptions? options = null) =>
        _builderService.AddAgents(options);

    public IAgentServiceBuilder AddAgents(Action<AgentOptions> optionsAction) =>
        _builderService.AddAgents(optionsAction);

    public IKernelMemoryServiceBuilder AddKernelMemory(KernelMemoryOptions? options = null) =>
        _builderService.AddKernelMemory(options);

    public IKernelMemoryServiceBuilder AddKernelMemory(Action<KernelMemoryOptions> optionsAction) =>
        _builderService.AddKernelMemory(optionsAction);

    public IMemoryServiceBuilder IncludeSimpleMemoryStore(SimpleMemoryStoreOptions? options = null)
    {
        if (options is not null)
        {
            Options.Store ??= new MemoryStoreOptions();
            Options.Store.SimpleMemoryStore = options;
        }

        Verify.ThrowInvalidOperationExceptionIfNull(
            Options.Store?.SimpleMemoryStore,
            $"No '{nameof(SimpleMemoryStoreOptions)}' provided.");

        Services.AddSingleton<IMemoryStoreFactory, SimpleMemoryStoreFactory>();

        return this;
    }

    public IMemoryServiceBuilder IncludeSimpleMemoryStore(Action<SimpleMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        Options.Store ??= new MemoryStoreOptions();
        Options.Store.SimpleMemoryStore ??= new SimpleMemoryStoreOptions();

        optionsAction(Options.Store.SimpleMemoryStore);

        return IncludeSimpleMemoryStore(Options.Store.SimpleMemoryStore);
    }
}
