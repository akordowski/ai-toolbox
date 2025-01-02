using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIToolbox.SemanticKernel;

public sealed class OpenAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly KernelOptions _kernelOptions;
    private readonly OpenAIConnectorOptions? _connectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public OpenAIKernelBuilderConfigurator(
        KernelOptions kernelOptions,
        OpenAIConnectorOptions? connectorOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(kernelOptions, nameof(kernelOptions));

        _kernelOptions = kernelOptions;
        _connectorOptions = connectorOptions;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureAudioToText(builder);
        ConfigureChatCompletion(builder);
        ConfigureTextEmbeddingGeneration(builder);
        ConfigureTextToAudio(builder);
        ConfigureTextToImage(builder);
        ConfigurePromptExecutionSettingsMapper(builder);
    }

    private void ConfigureAudioToText(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.OpenAI?.AudioToText;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.OrgId ??= _connectorOptions.OrgId;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddOpenAIAudioToText(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.OpenAI?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.OrgId ??= _connectorOptions.OrgId;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddOpenAIChatCompletion(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.OpenAI?.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.OrgId ??= _connectorOptions.OrgId;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddOpenAITextEmbeddingGeneration(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient(),
            dimensions: options.Dimensions);
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextToAudio(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.OpenAI?.TextToAudio;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.OrgId ??= _connectorOptions.OrgId;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddOpenAITextToAudio(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextToImage(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.OpenAI?.TextToImage;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.OrgId ??= _connectorOptions.OrgId;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddOpenAITextToImage(
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private static void ConfigurePromptExecutionSettingsMapper(IKernelBuilder builder)
    {
        // OpenAIPromptExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, OpenAIPromptExecutionSettingsMapper>(typeof(OpenAIChatCompletionService));

        // OpenAIAudioToTextExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, OpenAIAudioToTextExecutionSettingsMapper>(typeof(OpenAIAudioToTextService));

        // OpenAITextToAudioExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, OpenAITextToAudioExecutionSettingsMapper>(typeof(OpenAITextToAudioService));
    }
}
