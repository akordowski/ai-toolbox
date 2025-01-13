using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class ConnectorServiceBuilder : IConnectorServiceBuilder
{
    public ConnectorOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IServiceBuilderService _builderService;

    public ConnectorServiceBuilder(
        ConnectorOptions options,
        IServiceCollection services,
        IServiceBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;
    }

    public IKernelServiceBuilder AddKernel(KernelOptions? options = null) =>
        _builderService.AddKernel(options);

    public IKernelServiceBuilder AddKernel(Action<KernelOptions> optionsAction) =>
        _builderService.AddKernel(optionsAction);
}
