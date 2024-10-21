using AIToolbox.Options;
using AIToolbox.Options.Agents;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.KernelMemory;
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

    public IConnectorServiceBuilder AddConnectors(ConnectorOptions? options = null)
    {
        _options.Connectors ??= new ConnectorOptions();

        if (options is not null)
        {
            _options.Connectors = options;
        }

        return new ConnectorServiceBuilder(_options.Connectors!, _services, this);
    }

    public IConnectorServiceBuilder AddConnectors(Action<ConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Connectors ??= new ConnectorOptions();
        optionsAction(_options.Connectors);

        return AddConnectors(_options.Connectors!);
    }

    public IKernelServiceBuilder AddKernel(KernelOptions? options = null)
    {
        if (options is not null)
        {
            _options.Kernel = options;
        }

        return new KernelServiceBuilder(_options.Kernel!, _services, this);
    }

    public IKernelServiceBuilder AddKernel(Action<KernelOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Kernel ??= new KernelOptions();
        optionsAction(_options.Kernel);

        return AddKernel(_options.Kernel!);
    }

    public IMemoryServiceBuilder AddMemory(MemoryOptions? options = null)
    {
        if (options is not null)
        {
            _options.Memory = options;
        }

        return new MemoryServiceBuilder(_options.Memory!, _services, this);
    }

    public IMemoryServiceBuilder AddMemory(Action<MemoryOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Memory ??= new MemoryOptions();
        optionsAction(_options.Memory);

        return AddMemory(_options.Memory!);
    }

    public IKernelMemoryServiceBuilder AddKernelMemory(KernelMemoryOptions? options = null)
    {
        _options.KernelMemory ??= new KernelMemoryOptions();

        if (options is not null)
        {
            _options.KernelMemory = options;
        }

        return new KernelMemoryServiceBuilder(_options.KernelMemory!, _services, this);
    }

    public IKernelMemoryServiceBuilder AddKernelMemory(Action<KernelMemoryOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.KernelMemory ??= new KernelMemoryOptions();
        optionsAction(_options.KernelMemory);

        return AddKernelMemory(_options.KernelMemory!);
    }

    public IAgentServiceBuilder AddAgents(AgentOptions? options = null)
    {
        _options.Agents ??= new AgentOptions();

        if (options is not null)
        {
            _options.Agents = options;
        }

        return new AgentServiceBuilder(_options.Agents!, _services);
    }

    public IAgentServiceBuilder AddAgents(Action<AgentOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Agents ??= new AgentOptions();
        optionsAction(_options.Agents);

        return AddAgents(_options.Agents!);
    }
}
