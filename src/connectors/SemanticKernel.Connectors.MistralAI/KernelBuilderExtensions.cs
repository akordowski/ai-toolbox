using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithMistralAIConnector(
        this IKernelBuilder builder,
        MistralOptions? options = null)
    {
        Verify.ThrowIfNull(builder, nameof(builder));

        if (options is not null)
        {
            builder.Options.Connectors ??= new KernelConnectorOptions();
            builder.Options.Connectors.Mistral ??= options;
        }

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.Mistral);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.Mistral!)
            .AddSingleton<IKernelBuilderConfigurator, MistralAIKernelBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithMistralAIConnector(
        this IKernelBuilder builder,
        Action<MistralOptions> optionsAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new MistralOptions();
        optionsAction(options);

        return builder.WithMistralAIConnector(options);
    }
}
