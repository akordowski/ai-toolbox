using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorServiceBuilder IncludeOpenAIConnector(
        this IConnectorServiceBuilder builder,
        OpenAIConnectorOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.OpenAI ??= options;
        }

        if (opt.OpenAI is not null)
        {
            builder.Services.AddSingleton(opt.OpenAI);
        }

        builder.Services
            .AddSingleton<IKernelBuilderConfigurator, OpenAIKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, OpenAIMemoryBuilderConfigurator>();

        return builder;
    }

    public static IConnectorServiceBuilder IncludeOpenAIConnector(
        this IConnectorServiceBuilder builder,
        Action<OpenAIConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new OpenAIConnectorOptions();
        optionsAction(options);

        return builder.IncludeOpenAIConnector(options);
    }
}
