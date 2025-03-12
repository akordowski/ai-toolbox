using AIToolbox.Options.Connectors;
using AIToolbox.Options.Enums;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class GoogleKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly GoogleOptions _options;
    private readonly GlobalGoogleOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public GoogleKernelBuilderConfigurator(
        GoogleOptions options,
        GlobalGoogleOptions? globalOptions = null,
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

        ConfigureGoogleAIGeminiChatCompletion(builder);
        ConfigureGoogleAIEmbeddingGeneration(builder);
    }

    private void ConfigureGoogleAIGeminiChatCompletion(IKernelBuilder builder)
    {
        var options = _options.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.ApiKey ??= _globalOptions.ApiKey;
            options.ApiVersion ??= _globalOptions.ApiVersion;
        }

        builder.AddGoogleAIGeminiChatCompletion(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            apiVersion: GetGoogleAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureGoogleAIEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _options.EmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.ApiKey ??= _globalOptions.ApiKey;
            options.ApiVersion ??= _globalOptions.ApiVersion;
        }

        builder.AddGoogleAIEmbeddingGeneration(modelId: options.ModelId,
            apiKey: options.ApiKey!,
            apiVersion: GetGoogleAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private static Microsoft.SemanticKernel.Connectors.Google.GoogleAIVersion GetGoogleAIVersion(GoogleAIVersion apiVersion)
    {
        return apiVersion switch
        {
            GoogleAIVersion.V1 => Microsoft.SemanticKernel.Connectors.Google.GoogleAIVersion.V1,
            GoogleAIVersion.V1Beta => Microsoft.SemanticKernel.Connectors.Google.GoogleAIVersion.V1_Beta,
            _ => throw new ArgumentOutOfRangeException($"Invalid api version '{apiVersion}'.")
        };
    }
}
