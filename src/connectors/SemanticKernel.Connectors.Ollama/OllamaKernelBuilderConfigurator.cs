using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using OllamaSharp;

namespace AIToolbox.SemanticKernel;

internal sealed class OllamaKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly OllamaOptions _options;
    private readonly GlobalOllamaOptions? _globalOptions;
    private readonly OllamaApiClient? _ollamaApiClient;
    private readonly IHttpClientFactory? _httpClientFactory;

    public OllamaKernelBuilderConfigurator(
        OllamaOptions options,
        GlobalOllamaOptions? globalOptions = null,
        OllamaApiClient? ollamaApiClient = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
        _globalOptions = globalOptions;
        _ollamaApiClient = ollamaApiClient;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureChatCompletion(builder);
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
            options.ServiceId ??= _globalOptions.ServiceId;
        }

        if (_ollamaApiClient is not null)
        {
            builder.AddOllamaChatCompletion(
                ollamaClient: _ollamaApiClient,
                serviceId: options.ServiceId);
        }
        else if (_httpClientFactory is not null)
        {
            builder.AddOllamaChatCompletion(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                serviceId: options.ServiceId);
        }
        else
        {
            builder.AddOllamaChatCompletion(
                modelId: options.ModelId,
                endpoint: new Uri(options.Endpoint!),
                serviceId: options.ServiceId);
        }
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
            options.ServiceId ??= _globalOptions.ServiceId;
        }

        if (_ollamaApiClient is not null)
        {
            builder.AddOllamaTextEmbeddingGeneration(
                ollamaClient: _ollamaApiClient,
                serviceId: options.ServiceId);
        }
        else if (_httpClientFactory is not null)
        {
            builder.AddOllamaTextEmbeddingGeneration(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                serviceId: options.ServiceId);
        }
        else
        {
            builder.AddOllamaTextEmbeddingGeneration(
                modelId: options.ModelId,
                endpoint: new Uri(options.Endpoint!),
                serviceId: options.ServiceId);
        }
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
            options.ServiceId ??= _globalOptions.ServiceId;
        }

        if (_ollamaApiClient is not null)
        {
            builder.AddOllamaTextGeneration(
                modelId: options.ModelId,
                ollamaClient: _ollamaApiClient,
                serviceId: options.ServiceId);
        }
        else if (_httpClientFactory is not null)
        {
            builder.AddOllamaTextGeneration(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                serviceId: options.ServiceId);
        }
        else
        {
            builder.AddOllamaTextGeneration(
                modelId: options.ModelId,
                endpoint: new Uri(options.Endpoint!),
                serviceId: options.ServiceId);
        }
    }
}
