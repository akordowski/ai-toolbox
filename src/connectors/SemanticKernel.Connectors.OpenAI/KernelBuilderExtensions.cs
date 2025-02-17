using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithOpenAIConnector(
        this IKernelBuilder builder,
        OpenAIOptions? options = null)
    {
        Verify.ThrowIfNull(builder, nameof(builder));

        if (options is not null)
        {
            builder.Options.Connectors ??= new KernelConnectorOptions();
            builder.Options.Connectors.OpenAI ??= options;
        }

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.OpenAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.OpenAI!)
            .AddSingleton<IKernelBuilderConfigurator, OpenAIKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, OpenAIMemoryBuilderConfigurator>();

        return builder;
    }

    public static IKernelBuilder WithOpenAIConnector(
        this IKernelBuilder builder,
        Action<OpenAIOptions> optionsAction)
    {
        Verify.ThrowIfNull(builder, nameof(builder));
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new OpenAIOptions();
        optionsAction(options);

        return builder.WithOpenAIConnector(options);
    }
}
