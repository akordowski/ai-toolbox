using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public static class AIToolboxBuilderExtensions
{
    /// <summary>
    /// Adds a delegate for configuring an <see cref="ISemanticKernelBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IAIToolboxBuilder"/>.</param>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="ISemanticKernelBuilder"/>.</param>
    /// <returns>The value of <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public static IAIToolboxBuilder AddSemanticKernel(
        this IAIToolboxBuilder builder,
        Action<ISemanticKernelBuilder> builderAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));

        return builder.AddSemanticKernel(builderAction, _ => { });
    }

    /// <summary>
    /// Adds a delegate for configuring an <see cref="ISemanticKernelBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IAIToolboxBuilder"/>.</param>
    /// <param name="builderAction">>A delegate that is used to configure an <see cref="ISemanticKernelBuilder"/>.</param>
    /// <param name="optionsAction">A delegate that is used to configure the <see cref="SemanticKernelOptions"/>.</param>
    /// <returns>The value of <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
    public static IAIToolboxBuilder AddSemanticKernel(
        this IAIToolboxBuilder builder,
        Action<ISemanticKernelBuilder> builderAction,
        Action<SemanticKernelOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(builderAction, nameof(builderAction));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.SemanticKernel ??= new SemanticKernelOptions();

        var options = builder.Options.SemanticKernel;
        optionsAction.Invoke(options);

        var semanticKernelBuilder = new SemanticKernelBuilder(options, builder.Services);
        builderAction.Invoke(semanticKernelBuilder);

        return builder;
    }
}
