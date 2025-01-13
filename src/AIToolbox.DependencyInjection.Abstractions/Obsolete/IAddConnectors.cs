using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IAddConnectors
{
    IConnectorServiceBuilder AddConnectors(ConnectorOptions? options = null);
    IConnectorServiceBuilder AddConnectors(Action<ConnectorOptions> optionsAction);
}
