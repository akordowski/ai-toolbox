using AIToolbox.Options.Agents;
using AIToolbox.Options.KernelMemory;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.DependencyInjection;

internal sealed class KernelServiceBuilder : IKernelServiceBuilder
{
    public KernelOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IServiceBuilderService _builderService;

    public KernelServiceBuilder(
        KernelOptions options,
        IServiceCollection services,
        IServiceBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(KernelOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;

        Services
            .AddSingleton(Options)
            .AddSingleton<IKernelProvider, KernelProvider>();
    }

    public IAgentServiceBuilder AddAgents(AgentOptions? options = null) =>
        _builderService.AddAgents(options);

    public IAgentServiceBuilder AddAgents(Action<AgentOptions> optionsAction) =>
        _builderService.AddAgents(optionsAction);

    public IMemoryServiceBuilder AddMemory(MemoryOptions? options = null) =>
        _builderService.AddMemory(options);

    public IMemoryServiceBuilder AddMemory(Action<MemoryOptions> optionsAction) =>
        _builderService.AddMemory(optionsAction);

    public IKernelMemoryServiceBuilder AddKernelMemory(KernelMemoryOptions? options = null) =>
        _builderService.AddKernelMemory(options);

    public IKernelMemoryServiceBuilder AddKernelMemory(Action<KernelMemoryOptions> optionsAction) =>
        _builderService.AddKernelMemory(optionsAction);

    public IKernelServiceBuilder WithCustomAIServiceSelector(Func<IServiceProvider, IAIServiceSelector> factory)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IAIServiceSelector>(factory));
        return this;
    }

    public IKernelServiceBuilder WithCustomAIServiceSelector(IAIServiceSelector instance)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IAIServiceSelector>(instance));
        return this;
    }

    public IKernelServiceBuilder WithCustomFunctionInvocationFilter(Func<IServiceProvider, IFunctionInvocationFilter> factory)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IFunctionInvocationFilter>(factory));
        return this;
    }

    public IKernelServiceBuilder WithCustomFunctionInvocationFilter(IFunctionInvocationFilter instance)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IFunctionInvocationFilter>(instance));
        return this;
    }
}
