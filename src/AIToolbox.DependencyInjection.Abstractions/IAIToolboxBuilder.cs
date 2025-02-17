using AIToolbox.Options;
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
    /// Adds a delegate for configuring the <see cref="IGlobalConnectorBuilder"/>.
    /// </summary>
    /// <param name="builderAction">>A delegate for configuring the <see cref="IGlobalConnectorBuilder"/>.</param>
    /// <returns><see cref="IAIToolboxBuilder"/></returns>
    IAIToolboxBuilder ConfigureGlobalConnectorOptions(Action<IGlobalConnectorBuilder> builderAction);
}
