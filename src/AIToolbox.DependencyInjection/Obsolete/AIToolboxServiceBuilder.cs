using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

internal sealed class AIToolboxServiceBuilder : IAIToolboxServiceBuilder
{
    private readonly IServiceBuilderService _builderService;

    public AIToolboxServiceBuilder(IServiceBuilderService builderService)
    {
        Verify.ThrowIfNull(builderService, nameof(builderService));

        _builderService = builderService;
    }

    public IConnectorServiceBuilder AddConnectors(ConnectorOptions? options = null) =>
        _builderService.AddConnectors(options);

    public IConnectorServiceBuilder AddConnectors(Action<ConnectorOptions> optionsAction) =>
        _builderService.AddConnectors(optionsAction);
}
