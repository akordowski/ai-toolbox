using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorServiceBuilder IncludeGoogleConnector(
        this IConnectorServiceBuilder builder,
        GlobalGoogleOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.Google ??= options;
        }

        if (opt.Google is not null)
        {
            builder.Services.AddSingleton(opt.Google);
        }

        builder.Services.AddSingleton<IKernelBuilderConfigurator, GoogleKernelBuilderConfigurator>();

        return builder;
    }

    public static IConnectorServiceBuilder IncludeGoogleConnector(
        this IConnectorServiceBuilder builder,
        Action<GlobalGoogleOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new GlobalGoogleOptions();
        optionsAction(options);

        return builder.IncludeGoogleConnector(options);
    }
}
