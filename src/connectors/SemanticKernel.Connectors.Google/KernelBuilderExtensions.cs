using AIToolbox.DependencyInjection;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.SemanticKernel;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithGoogleConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.Google);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.Google!)
            .AddSingleton<IKernelBuilderConfigurator, GoogleKernelBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithGoogleConnector(
        this IKernelBuilder builder,
        Action<GoogleOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.Google ??= new GoogleOptions();

        optionsAction(builder.Options.Connectors.Google);

        return builder.WithGoogleConnector();
    }

    public static IKernelBuilder WithVertexAIConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.VertexAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.VertexAI!)
            .AddSingleton<IKernelBuilderConfigurator, VertexAIKernelBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithVertexAIConnector(
        this IKernelBuilder builder,
        Action<VertexAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.VertexAI ??= new VertexAIOptions();

        optionsAction(builder.Options.Connectors.VertexAI);

        return builder.WithVertexAIConnector();
    }
}
