using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithAzureOpenAIConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.AzureOpenAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.AzureOpenAI!)
            .AddSingleton<IKernelBuilderConfigurator, AzureOpenAIKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, AzureOpenAIMemoryBuilderConfigurator>()
            .AddKeyedSingleton<IPromptExecutionSettingsMapper, AzureOpenAIPromptExecutionSettingsMapper>(typeof(AzureOpenAIChatCompletionService));

        return builder;
    }

    public static IKernelBuilder WithAzureOpenAIConnector(
        this IKernelBuilder builder,
        Action<AzureOpenAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.AzureOpenAI ??= new AzureOpenAIOptions();

        optionsAction(builder.Options.Connectors.AzureOpenAI);

        return builder.WithAzureOpenAIConnector();
    }
}
