using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

internal sealed class AIToolboxBuilder : IAIToolboxBuilder
{
    /// <inheritdoc />
    public IBuilderFactory BuilderFactory { get; }

    public AIToolboxBuilder(IBuilderFactory builderFactory)
    {
        Verify.ThrowIfNull(builderFactory, nameof(builderFactory));

        BuilderFactory = builderFactory;
    }

    /// <inheritdoc />
    public IAIToolboxBuilder ConfigureConnectors(GlobalConnectorOptions options)
    {
        Verify.ThrowIfNull(options, nameof(options));

        BuilderFactory.Options.GlobalConnectors = options;

        return this;
    }

    /// <inheritdoc />
    public IAIToolboxBuilder ConfigureConnectors(Action<GlobalConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        BuilderFactory.Options.GlobalConnectors ??= new GlobalConnectorOptions();
        optionsAction(BuilderFactory.Options.GlobalConnectors);

        return this;
    }
}
