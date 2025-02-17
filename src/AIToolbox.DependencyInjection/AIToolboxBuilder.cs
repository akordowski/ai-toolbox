using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class AIToolboxBuilder : IAIToolboxBuilder
{
    /// <inheritdoc />
    public AIToolboxOptions Options { get; }

    /// <inheritdoc />
    public IServiceCollection Services { get; }

    public AIToolboxBuilder(AIToolboxOptions options, IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;
    }

    /// <inheritdoc />
    public IAIToolboxBuilder ConfigureGlobalConnectorOptions(Action<IGlobalConnectorBuilder> builderAction)
    {
        Verify.ThrowIfNull(builderAction, nameof(builderAction));

        Options.GlobalConnectors ??= new GlobalConnectorOptions();
        builderAction(new GlobalConnectorBuilder(Options.GlobalConnectors, Services));

        return this;
    }
}
