using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace AIToolbox.SemanticKernel;

public sealed class AzureOpenAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly KernelOptions _kernelOptions;
    private readonly GlobalAzureOpenAIOptions? _connectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public AzureOpenAIKernelBuilderConfigurator(
        KernelOptions kernelOptions,
        GlobalAzureOpenAIOptions? connectorOptions = null,
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
        var options = _kernelOptions.Connectors?.AzureOpenAI?.AudioToText;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.Endpoint ??= _connectorOptions.Endpoint;
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddAzureOpenAIAudioToText(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.AzureOpenAI?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.Endpoint ??= _connectorOptions.Endpoint;
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddAzureOpenAIChatCompletion(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.AzureOpenAI?.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.Endpoint ??= _connectorOptions.Endpoint;
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddAzureOpenAITextEmbeddingGeneration(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient(),
            dimensions: options.Dimensions);
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextToAudio(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.AzureOpenAI?.TextToAudio;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.Endpoint ??= _connectorOptions.Endpoint;
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddAzureOpenAITextToAudio(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureTextToImage(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.AzureOpenAI?.TextToImage;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.Endpoint ??= _connectorOptions.Endpoint;
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.ServiceId ??= _connectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddAzureOpenAITextToImage(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private static void ConfigurePromptExecutionSettingsMapper(IKernelBuilder builder)
    {
        // OpenAIPromptExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, AzureOpenAIPromptExecutionSettingsMapper>(typeof(AzureOpenAIChatCompletionService));

        // OpenAIAudioToTextExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, AzureOpenAIAudioToTextExecutionSettingsMapper>(typeof(AzureOpenAIAudioToTextService));

        // OpenAITextToAudioExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, AzureOpenAITextToAudioExecutionSettingsMapper>(typeof(AzureOpenAITextToAudioService));
    }
}
