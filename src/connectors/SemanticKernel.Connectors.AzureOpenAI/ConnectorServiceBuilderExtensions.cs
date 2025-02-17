using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorServiceBuilder IncludeAzureOpenAIConnector(
        this IConnectorServiceBuilder builder,
        GlobalAzureOpenAIOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.AzureOpenAI ??= options;
        }

        if (opt.AzureOpenAI is not null)
        {
            builder.Services.AddSingleton(opt.AzureOpenAI);
        }

        builder.Services
            .AddSingleton<IKernelBuilderConfigurator, AzureOpenAIKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, AzureOpenAIMemoryBuilderConfigurator>();

        return builder;
    }

    public static IConnectorServiceBuilder IncludeAzureOpenAIConnector(
        this IConnectorServiceBuilder builder,
        Action<GlobalAzureOpenAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new GlobalAzureOpenAIOptions();
        optionsAction(options);

        return builder.IncludeAzureOpenAIConnector(options);
    }
}
