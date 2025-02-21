using AIToolbox.DependencyInjection;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.SemanticKernel;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithAzureOpenAIConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.AzureOpenAI);

        builder.Services.AddSingleton(builder.Options.Connectors!.AzureOpenAI!);

        return builder;
    }

    public static IKernelBuilder WithAzureOpenAIConnector(
        this IKernelBuilder builder,
        Action<AzureOpenAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.AzureOpenAI ??= new AzureOpenAIOptions();

        optionsAction(builder.Options.Connectors.AzureOpenAI);

        return builder.WithAzureOpenAIConnector();
    }
}
