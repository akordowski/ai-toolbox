using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorServiceBuilder IncludeOllamaConnector(
        this IConnectorServiceBuilder builder,
        OllamaConnectorOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.Ollama ??= options;
        }

        if (opt.Ollama is not null)
        {
            builder.Services.AddSingleton(opt.Ollama);
        }

        builder.Services
            .AddSingleton<IKernelBuilderConfigurator, OllamaKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, OllamaMemoryBuilderConfigurator>();

        return builder;
    }

    public static IConnectorServiceBuilder IncludeOllamaConnector(
        this IConnectorServiceBuilder builder,
        Action<OllamaConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new OllamaConnectorOptions();
        optionsAction(options);

        return builder.IncludeOllamaConnector(options);
    }
}
