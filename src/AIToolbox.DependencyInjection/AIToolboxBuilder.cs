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
    public IAIToolboxBuilder ConfigureConnectors(ConnectorOptions options)
    {
        Verify.ThrowIfNull(options, nameof(options));

        BuilderFactory.Options.Connectors = options;

        return this;
    }

    /// <inheritdoc />
    public IAIToolboxBuilder ConfigureConnectors(Action<ConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        BuilderFactory.Options.Connectors ??= new ConnectorOptions();
        optionsAction(BuilderFactory.Options.Connectors);

        return this;
    }
}
