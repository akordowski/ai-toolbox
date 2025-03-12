using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class OpenAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly OpenAIOptions _options;
    private readonly GlobalOpenAIOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public OpenAIKernelBuilderConfigurator(
        OpenAIOptions options,
        GlobalOpenAIOptions? globalOptions = null,
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
            options.ApiKey ??= _globalOptions.ApiKey;
            options.OrgId ??= _globalOptions.OrgId;
        }

        builder.AddOpenAIAudioToText(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
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
            options.ApiKey ??= _globalOptions.ApiKey;
            options.OrgId ??= _globalOptions.OrgId;
        }

        builder.AddOpenAIChatCompletion(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
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
            options.ApiKey ??= _globalOptions.ApiKey;
            options.OrgId ??= _globalOptions.OrgId;
        }

        builder.AddOpenAITextEmbeddingGeneration(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
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
            options.ApiKey ??= _globalOptions.ApiKey;
            options.OrgId ??= _globalOptions.OrgId;
        }

        builder.AddOpenAITextToAudio(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
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
            options.ApiKey ??= _globalOptions.ApiKey;
            options.OrgId ??= _globalOptions.OrgId;
        }

        builder.AddOpenAITextToImage(
            apiKey: options.ApiKey!,
            orgId: options.OrgId,
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }
}
