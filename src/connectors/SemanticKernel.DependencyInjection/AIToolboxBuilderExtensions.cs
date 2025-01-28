using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public static class AIToolboxBuilderExtensions
{
    public static IAIToolboxBuilder UseSemanticKernel(
        this IAIToolboxBuilder builder,
        Action<ISemanticKernelBuilder> builderAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(builderAction, nameof(builderAction));

        builder.BuilderFactory.Options.SemanticKernel ??= new SemanticKernelOptions();
        builderAction.Invoke(GetSemanticKernelBuilder(builder));

        return builder;
    }

    public static IAIToolboxBuilder UseSemanticKernel(
        this IAIToolboxBuilder builder,
        Action<ISemanticKernelBuilder> builderAction,
        SemanticKernelOptions options)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(builderAction, nameof(builderAction));
        Verify.ThrowIfNull(options, nameof(options));

        builder.BuilderFactory.Options.SemanticKernel = options;
        builderAction.Invoke(GetSemanticKernelBuilder(builder));

        return builder;
    }

    public static IAIToolboxBuilder UseSemanticKernel(
        this IAIToolboxBuilder builder,
        Action<ISemanticKernelBuilder> builderAction,
        Action<SemanticKernelOptions> optionsAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(builderAction, nameof(builderAction));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.BuilderFactory.Options.SemanticKernel ??= new SemanticKernelOptions();

        optionsAction.Invoke(builder.BuilderFactory.Options.SemanticKernel);
        builderAction.Invoke(GetSemanticKernelBuilder(builder));

        return builder;
    }

    private static SemanticKernelBuilder GetSemanticKernelBuilder(IAIToolboxBuilder builder) =>
        new(builder.BuilderFactory.Options.SemanticKernel!, builder.BuilderFactory.Services);
}
