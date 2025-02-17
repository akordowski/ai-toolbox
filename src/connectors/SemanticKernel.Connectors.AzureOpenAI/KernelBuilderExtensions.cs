using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithAzureOpenAIConnector(
        this IKernelBuilder builder,
        AzureOpenAIOptions? options = null)
    {
        Verify.ThrowIfNull(builder, nameof(builder));

        if (options is not null)
        {
            builder.Options.Connectors ??= new KernelConnectorOptions();
            builder.Options.Connectors.AzureOpenAI ??= options;
        }

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.AzureOpenAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.AzureOpenAI!)
            .AddSingleton<IKernelBuilderConfigurator, AzureOpenAIKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, AzureOpenAIMemoryBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithAzureOpenAIConnector(
        this IKernelBuilder builder,
        Action<AzureOpenAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AzureOpenAIOptions();
        optionsAction(options);

        return builder.WithAzureOpenAIConnector(options);
    }
}
