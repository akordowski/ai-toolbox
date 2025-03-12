using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class HuggingFaceKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly HuggingFaceOptions _options;
    private readonly GlobalHuggingFaceOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public HuggingFaceKernelBuilderConfigurator(
        HuggingFaceOptions options,
        GlobalHuggingFaceOptions? globalOptions = null,
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

        ConfigureChatCompletion(builder);
        ConfigureImageToText(builder);
        ConfigureTextEmbeddingGeneration(builder);
        ConfigureTextGeneration(builder);
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

        builder.AddHuggingFaceChatCompletion(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureImageToText(IKernelBuilder builder)
    {
        var options = _options.ImageToText;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddHuggingFaceImageToText(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
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

        builder.AddHuggingFaceTextEmbeddingGeneration(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureTextGeneration(IKernelBuilder builder)
    {
        var options = _options.TextGeneration;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
        }

        builder.AddHuggingFaceTextGeneration(
            model: options.Model,
            endpoint: new Uri(options.Endpoint!),
            apiKey: options.ApiKey,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }
}
