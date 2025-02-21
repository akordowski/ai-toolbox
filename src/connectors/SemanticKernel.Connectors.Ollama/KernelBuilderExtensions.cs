using AIToolbox.DependencyInjection;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.SemanticKernel;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithOllamaConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.Ollama);

        builder.Services.AddSingleton(builder.Options.Connectors!.Ollama!);

        return builder;
    }

    public static IKernelBuilder WithOllamaConnector(
        this IKernelBuilder builder,
        Action<OllamaOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.Ollama ??= new OllamaOptions();

        optionsAction(builder.Options.Connectors.Ollama);

        return builder.WithOllamaConnector();
    }
}
