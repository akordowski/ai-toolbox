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
    public IAIToolboxBuilder ConfigureConnectors(GlobalConnectorOptions options)
    {
        Verify.ThrowIfNull(options, nameof(options));

        Options.GlobalConnectors = options;

        return this;
    }

    /// <inheritdoc />
    public IAIToolboxBuilder ConfigureConnectors(Action<GlobalConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        Options.GlobalConnectors ??= new GlobalConnectorOptions();
        optionsAction(Options.GlobalConnectors);

        return this;
    }
}
