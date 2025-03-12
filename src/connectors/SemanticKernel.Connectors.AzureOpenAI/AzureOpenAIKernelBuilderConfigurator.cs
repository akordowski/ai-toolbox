using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class AzureOpenAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly AzureOpenAIOptions _options;
    private readonly GlobalAzureOpenAIOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public AzureOpenAIKernelBuilderConfigurator(
        AzureOpenAIOptions options,
        GlobalAzureOpenAIOptions? globalOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
        _globalOptions = globalOptions;
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
    }

    private void ConfigureAudioToText(IKernelBuilder builder)
    {
        var options = _options.AudioToText;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddAzureOpenAIAudioToText(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureChatCompletion(IKernelBuilder builder)
    {
        var options = _options.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddAzureOpenAIChatCompletion(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureTextEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _options.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddAzureOpenAITextEmbeddingGeneration(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient(),
            dimensions: options.Dimensions);
    }

    private void ConfigureTextToAudio(IKernelBuilder builder)
    {
        var options = _options.TextToAudio;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddAzureOpenAITextToAudio(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureTextToImage(IKernelBuilder builder)
    {
        var options = _options.TextToImage;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddAzureOpenAITextToImage(
            deploymentName: options.DeploymentName,
            endpoint: options.Endpoint!,
            apiKey: options.ApiKey!,
            serviceId: options.ServiceId,
            modelId: options.ModelId,
            httpClient: _httpClientFactory?.CreateClient());
    }
}
