using AIToolbox.Options;
using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IAIToolboxBuilder
{
    /// <summary>
    /// Gets the <see cref="IBuilderFactory"/>.
    /// </summary>
    IBuilderFactory BuilderFactory { get; }

    /// <summary>
    /// Add or replace the <see cref="ConnectorOptions"/> in the <see cref="AIToolboxOptions"/>.
    /// </summary>
    /// <param name="options">The <see cref="ConnectorOptions"/>.</param>
    /// <returns><see cref="IAIToolboxBuilder"/></returns>
    IAIToolboxBuilder ConfigureConnectors(ConnectorOptions options);

    /// <summary>
    /// Adds a delegate for configuring the <see cref="ConnectorOptions"/> in the <see cref="AIToolboxOptions"/>.
    /// </summary>
    /// <param name="optionsAction">>A delegate for configuring the <see cref="ConnectorOptions"/>.</param>
    /// <returns><see cref="IAIToolboxBuilder"/></returns>
    IAIToolboxBuilder ConfigureConnectors(Action<ConnectorOptions> optionsAction);
}
