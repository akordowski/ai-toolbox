using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorServiceBuilder IncludeHuggingFaceConnector(
        this IConnectorServiceBuilder builder,
        HuggingFaceConnectorOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.HuggingFace ??= options;
        }

        if (opt.HuggingFace is not null)
        {
            builder.Services.AddSingleton(opt.HuggingFace);
        }

        builder.Services.AddSingleton<IKernelBuilderConfigurator, HuggingFaceKernelBuilderConfigurator>();

        return builder;
    }

    public static IConnectorServiceBuilder IncludeHuggingFaceConnector(
        this IConnectorServiceBuilder builder,
        Action<HuggingFaceConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new HuggingFaceConnectorOptions();
        optionsAction(options);

        return builder.IncludeHuggingFaceConnector(options);
    }
}
