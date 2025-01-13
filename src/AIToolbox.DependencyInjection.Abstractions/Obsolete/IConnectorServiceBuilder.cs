using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IConnectorServiceBuilder : IServiceBuilder<ConnectorOptions>, IAddKernel;
