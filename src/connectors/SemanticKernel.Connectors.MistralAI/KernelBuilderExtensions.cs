using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithMistralAIConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.MistralAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.MistralAI!)
            .AddSingleton<IKernelBuilderConfigurator, MistralAIKernelBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithMistralAIConnector(
        this IKernelBuilder builder,
        Action<MistralAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.MistralAI ??= new MistralAIOptions();

        optionsAction(builder.Options.Connectors.MistralAI);

        return builder.WithMistralAIConnector();
    }
}
