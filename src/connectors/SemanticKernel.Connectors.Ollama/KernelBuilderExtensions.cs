using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithOllamaConnector(
        this IKernelBuilder builder,
        OllamaOptions? options = null)
    {
        Verify.ThrowIfNull(builder, nameof(builder));

        if (options is not null)
        {
            builder.Options.Connectors ??= new KernelConnectorOptions();
            builder.Options.Connectors.Ollama ??= options;
        }

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.Ollama);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.Ollama!)
            .AddSingleton<IKernelBuilderConfigurator, OllamaKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, OllamaMemoryBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithOllamaConnector(
        this IKernelBuilder builder,
        Action<OllamaOptions> optionsAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new OllamaOptions();
        optionsAction(options);

        return builder.WithOllamaConnector(options);
    }
}
