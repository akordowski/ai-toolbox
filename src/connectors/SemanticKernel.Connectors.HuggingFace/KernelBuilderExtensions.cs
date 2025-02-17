using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithHuggingFaceConnector(
        this IKernelBuilder builder,
        HuggingFaceOptions? options = null)
    {
        Verify.ThrowIfNull(builder, nameof(builder));

        if (options is not null)
        {
            builder.Options.Connectors ??= new KernelConnectorOptions();
            builder.Options.Connectors.HuggingFace ??= options;
        }

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
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new HuggingFaceOptions();
        optionsAction(options);

        return builder.WithHuggingFaceConnector(options);
    }
}
