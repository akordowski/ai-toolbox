using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithGoogleConnector(
        this IKernelBuilder builder,
        GoogleAIOptions? options = null)
    {
        Verify.ThrowIfNull(builder, nameof(builder));

        if (options is not null)
        {
            builder.Options.Connectors ??= new KernelConnectorOptions();
            builder.Options.Connectors.GoogleAI ??= options;
        }

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.GoogleAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.GoogleAI!)
            .AddSingleton<IKernelBuilderConfigurator, GoogleKernelBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithGoogleConnector(
        this IKernelBuilder builder,
        Action<GoogleAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new GoogleAIOptions();
        optionsAction(options);

        return builder.WithGoogleConnector(options);
    }
}
