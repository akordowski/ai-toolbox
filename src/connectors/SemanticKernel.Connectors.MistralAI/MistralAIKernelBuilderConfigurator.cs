using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class MistralAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly MistralAIOptions _options;
    private readonly GlobalMistralAIOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public MistralAIKernelBuilderConfigurator(
        MistralAIOptions options,
        GlobalMistralAIOptions? globalOptions = null,
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

        ConfigureMistralChatCompletion(builder);
        ConfigureMistralTextEmbeddingGeneration(builder);
    }

    private void ConfigureMistralChatCompletion(IKernelBuilder builder)
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

        builder.AddMistralChatCompletion(
            modelId: options.Model,
            apiKey: options.ApiKey!,
            endpoint: new Uri(options.Endpoint!),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureMistralTextEmbeddingGeneration(IKernelBuilder builder)
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

        builder.AddMistralTextEmbeddingGeneration(
            modelId: options.Model,
            apiKey: options.ApiKey!,
            endpoint: new Uri(options.Endpoint!),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }
}
