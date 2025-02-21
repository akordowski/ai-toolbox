using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithHuggingFaceConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.HuggingFace);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.HuggingFace!)
            .AddSingleton<IKernelBuilderConfigurator, HuggingFaceKernelBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithHuggingFaceConnector(
        this IKernelBuilder builder,
        Action<HuggingFaceOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.HuggingFace ??= new HuggingFaceOptions();

        optionsAction(builder.Options.Connectors.HuggingFace);

        return builder.WithHuggingFaceConnector();
    }
}
