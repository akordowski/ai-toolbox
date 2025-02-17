using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class ServiceBuilderService : IServiceBuilderService
{
    private readonly AIToolboxOptions _options;
    private readonly IServiceCollection _services;

    public ServiceBuilderService(
        AIToolboxOptions options,
        IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }

    public IConnectorServiceBuilder AddConnectors(GlobalConnectorOptions? options = null)
    {
        _options.GlobalConnectors ??= new GlobalConnectorOptions();

        if (options is not null)
        {
            _options.GlobalConnectors = options;
        }

        return new ConnectorServiceBuilder(_options.GlobalConnectors!, _services, this);
    }

    public IConnectorServiceBuilder AddConnectors(Action<GlobalConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.GlobalConnectors ??= new GlobalConnectorOptions();
        optionsAction(_options.GlobalConnectors);

        return AddConnectors(_options.GlobalConnectors!);
    }

    public IKernelServiceBuilder AddKernel(KernelOptions? options = null)
    {
        if (options is not null)
        {
            _options.SemanticKernel ??= new SemanticKernelOptions();
            _options.SemanticKernel.Kernel = options;
        }

        return new KernelServiceBuilder(_options.SemanticKernel?.Kernel!, _services, this);
    }

    public IKernelServiceBuilder AddKernel(Action<KernelOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.SemanticKernel ??= new SemanticKernelOptions();
        _options.SemanticKernel.Kernel ??= new KernelOptions();

        optionsAction(_options.SemanticKernel.Kernel);

        return AddKernel(_options.SemanticKernel.Kernel);
    }

    public IMemoryServiceBuilder AddMemory(MemoryOptions? options = null)
    {
        if (options is not null)
        {
            _options.SemanticKernel ??= new SemanticKernelOptions();
            _options.SemanticKernel.Memory = options;
        }

        return new MemoryServiceBuilder(_options.SemanticKernel?.Memory!, _services, this);
    }

    public IMemoryServiceBuilder AddMemory(Action<MemoryOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.SemanticKernel ??= new SemanticKernelOptions();
        _options.SemanticKernel.Memory ??= new MemoryOptions();

        optionsAction(_options.SemanticKernel.Memory);

        return AddMemory(_options.SemanticKernel.Memory);
    }

    public IAgentServiceBuilder AddAgents(AgentOptions? options = null)
    {
        _options.SemanticKernel ??= new SemanticKernelOptions();
        _options.SemanticKernel.Agents ??= new AgentOptions();

        if (options is not null)
        {
            _options.SemanticKernel.Agents = options;
        }

        return new AgentServiceBuilder(_options.SemanticKernel.Agents!, _services);
    }

    public IAgentServiceBuilder AddAgents(Action<AgentOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.SemanticKernel ??= new SemanticKernelOptions();
        _options.SemanticKernel.Agents ??= new AgentOptions();

        optionsAction(_options.SemanticKernel.Agents);

        return AddAgents(_options.SemanticKernel.Agents);
    }
}
