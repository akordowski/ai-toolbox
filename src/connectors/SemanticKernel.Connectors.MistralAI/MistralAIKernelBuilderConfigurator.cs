using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.MistralAI;

namespace AIToolbox.SemanticKernel;

public sealed class MistralAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly KernelOptions _kernelOptions;
    private readonly MistralAIConnectorOptions? _mistralAIConnectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public MistralAIKernelBuilderConfigurator(
        KernelOptions kernelOptions,
        MistralAIConnectorOptions? mistralAIConnectorOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(kernelOptions, nameof(kernelOptions));

        _kernelOptions = kernelOptions;
        _mistralAIConnectorOptions = mistralAIConnectorOptions;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureMistralChatCompletion(builder);
        ConfigureMistralTextEmbeddingGeneration(builder);
        ConfigurePromptExecutionSettingsMapper(builder);
    }

    private void ConfigureMistralChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.Mistral?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_mistralAIConnectorOptions is not null)
        {
            options.Endpoint ??= _mistralAIConnectorOptions.Endpoint;
            options.ApiKey ??= _mistralAIConnectorOptions.ApiKey;
            options.ServiceId ??= _mistralAIConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddMistralChatCompletion(
            modelId: options.Model,
            apiKey: options.ApiKey!,
            endpoint: new Uri(options.Endpoint!),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureMistralTextEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.Mistral?.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_mistralAIConnectorOptions is not null)
        {
            options.Endpoint ??= _mistralAIConnectorOptions.Endpoint;
            options.ApiKey ??= _mistralAIConnectorOptions.ApiKey;
            options.ServiceId ??= _mistralAIConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddMistralTextEmbeddingGeneration(
            modelId: options.Model,
            apiKey: options.ApiKey!,
            endpoint: new Uri(options.Endpoint!),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private static void ConfigurePromptExecutionSettingsMapper(IKernelBuilder builder)
    {
        // MistralAIPromptExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, MistralAIPromptExecutionSettingsMapper>(typeof(MistralAIChatCompletionService));
    }
}
