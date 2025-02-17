using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorServiceBuilder IncludeMistralAIConnector(
        this IConnectorServiceBuilder builder,
        GlobalMistralAIOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.MistralAI ??= options;
        }

        if (opt.MistralAI is not null)
        {
            builder.Services.AddSingleton(opt.MistralAI);
        }

        builder.Services.AddSingleton<IKernelBuilderConfigurator, MistralAIKernelBuilderConfigurator>();

        return builder;
    }

    public static IConnectorServiceBuilder IncludeMistralAIConnector(
        this IConnectorServiceBuilder builder,
        Action<GlobalMistralAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new GlobalMistralAIOptions();
        optionsAction(options);

        return builder.IncludeMistralAIConnector(options);
    }
}
