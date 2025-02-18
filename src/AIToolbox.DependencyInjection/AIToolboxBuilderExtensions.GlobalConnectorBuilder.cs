using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public static class AIToolboxBuilderExtensions
{
    /// <summary>
    /// Adds a delegate for configuring an <see cref="IGlobalConnectorBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IAIToolboxBuilder"/>.</param>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="IGlobalConnectorBuilder"/>.</param>
    /// <returns>The value of <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public static IAIToolboxBuilder ConfigureGlobalConnectorOptions(
        this IAIToolboxBuilder builder,
        Action<IGlobalConnectorBuilder> builderAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));

        return builder.ConfigureGlobalConnectorOptions(builderAction, _ => { });
    }

    /// <summary>
    /// Adds a delegate for configuring an <see cref="IGlobalConnectorBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IAIToolboxBuilder"/>.</param>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="IGlobalConnectorBuilder"/>.</param>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="GlobalConnectorOptions"/>.</param>
    /// <returns>The value of <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public static IAIToolboxBuilder ConfigureGlobalConnectorOptions(
        this IAIToolboxBuilder builder,
        Action<IGlobalConnectorBuilder> builderAction,
        Action<GlobalConnectorOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.GlobalConnectors ??= new GlobalConnectorOptions();

        var options = builder.Options.GlobalConnectors;
        optionsAction.Invoke(options);

        var globalConnectorBuilder = new GlobalConnectorBuilder(options, builder.Services);
        builderAction.Invoke(globalConnectorBuilder);

        return builder;
    }
}
