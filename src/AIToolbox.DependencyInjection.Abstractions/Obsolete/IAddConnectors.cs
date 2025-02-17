using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IAddConnectors
{
    IConnectorServiceBuilder AddConnectors(GlobalConnectorOptions? options = null);
    IConnectorServiceBuilder AddConnectors(Action<GlobalConnectorOptions> optionsAction);
}
