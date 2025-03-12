using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class KernelBuilderExtensions
{
    public static IKernelBuilder WithOpenAIConnector(this IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        Verify.ThrowIfOptionsNull(builder.Options.Connectors?.OpenAI);

        builder.Services
            .AddSingleton(builder.Options.Connectors!.OpenAI!)
            .AddSingleton<IKernelBuilderConfigurator, OpenAIKernelBuilderConfigurator>()
            .AddSingleton<IMemoryBuilderConfigurator, OpenAIMemoryBuilderConfigurator>()
            .AddKeyedSingleton<IPromptExecutionSettingsMapper, OpenAIPromptExecutionSettingsMapper>(typeof(OpenAIChatCompletionService))
            .AddKeyedSingleton<IPromptExecutionSettingsMapper, OpenAIAudioToTextExecutionSettingsMapper>(typeof(OpenAIAudioToTextService))
            .AddKeyedSingleton<IPromptExecutionSettingsMapper, OpenAITextToAudioExecutionSettingsMapper>(typeof(OpenAITextToAudioService));

        return builder;
    }

    public static IKernelBuilder WithOpenAIConnector(
        this IKernelBuilder builder,
        Action<OpenAIOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        builder.Options.Connectors ??= new ConnectorOptions();
        builder.Options.Connectors.OpenAI ??= new OpenAIOptions();

        optionsAction(builder.Options.Connectors.OpenAI);

        return builder.WithOpenAIConnector();
    }
}
