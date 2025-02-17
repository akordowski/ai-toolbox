using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public interface IAIToolboxBuilder
{
    /// <summary>
    /// Gets the <see cref="AIToolboxOptions"/>.
    /// </summary>
    AIToolboxOptions Options { get; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> where AIToolbox services are configured.
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// Add or replace the <see cref="GlobalConnectorOptions"/> in the <see cref="AIToolboxOptions"/>.
    /// </summary>
    /// <param name="options">The <see cref="GlobalConnectorOptions"/>.</param>
    /// <returns><see cref="IAIToolboxBuilder"/></returns>
    IAIToolboxBuilder ConfigureConnectors(GlobalConnectorOptions options);

    /// <summary>
    /// Adds a delegate for configuring the <see cref="GlobalConnectorOptions"/> in the <see cref="AIToolboxOptions"/>.
    /// </summary>
    /// <param name="optionsAction">>A delegate for configuring the <see cref="GlobalConnectorOptions"/>.</param>
    /// <returns><see cref="IAIToolboxBuilder"/></returns>
    IAIToolboxBuilder ConfigureConnectors(Action<GlobalConnectorOptions> optionsAction);
}
