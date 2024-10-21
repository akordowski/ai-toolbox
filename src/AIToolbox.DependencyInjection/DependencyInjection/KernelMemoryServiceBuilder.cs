using AIToolbox.KernelMemory;
using AIToolbox.Options.Agents;
using AIToolbox.Options.KernelMemory;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class KernelMemoryServiceBuilder : IKernelMemoryServiceBuilder
{
    public KernelMemoryOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IServiceBuilderService _builderService;

    public KernelMemoryServiceBuilder(
        KernelMemoryOptions options,
        IServiceCollection services,
        IServiceBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(KernelMemoryOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;

        Services
            .AddSingleton(Options)
            .AddSingleton<IKernelMemoryProvider, KernelMemoryProvider>();
    }

    public IAgentServiceBuilder AddAgents(AgentOptions? options = null) =>
        _builderService.AddAgents(options);

    public IAgentServiceBuilder AddAgents(Action<AgentOptions> optionsAction) =>
        _builderService.AddAgents(optionsAction);

    public IMemoryServiceBuilder AddMemory(MemoryOptions? options = null) =>
        _builderService.AddMemory(options);

    public IMemoryServiceBuilder AddMemory(Action<MemoryOptions> optionsAction) =>
        _builderService.AddMemory(optionsAction);
}
