using AIToolbox.Options.Connectors;
using AIToolbox.Options.Enums;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal sealed class VertexAIKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly VertexAIOptions _options;
    private readonly GlobalVertexAIOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public VertexAIKernelBuilderConfigurator(
        VertexAIOptions options,
        GlobalVertexAIOptions? globalOptions = null,
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

        ConfigureVertexAIGeminiChatCompletion(builder);
        ConfigureVertexAIEmbeddingGeneration(builder);
    }

    private void ConfigureVertexAIGeminiChatCompletion(IKernelBuilder builder)
    {
        var options = _options.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.BearerKey ??= _globalOptions.BearerKey;
            options.Location ??= _globalOptions.Location;
            options.ProjectId ??= _globalOptions.ProjectId;
            options.ApiVersion ??= _globalOptions.ApiVersion;
            options.ServiceId ??= _globalOptions.ServiceId;
        }

        builder.AddVertexAIGeminiChatCompletion(
            modelId: options.ModelId,
            bearerKey: options.BearerKey!,
            location: options.Location!,
            projectId: options.ProjectId!,
            apiVersion: GetVertexAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private void ConfigureVertexAIEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _options.EmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.BearerKey ??= _globalOptions.BearerKey;
            options.Location ??= _globalOptions.Location;
            options.ProjectId ??= _globalOptions.ProjectId;
            options.ApiVersion ??= _globalOptions.ApiVersion;
            options.ServiceId ??= _globalOptions.ServiceId;
        }

        builder.AddVertexAIEmbeddingGeneration(
            modelId: options.ModelId,
            bearerKey: options.BearerKey!,
            location: options.Location!,
            projectId: options.ProjectId!,
            apiVersion: GetVertexAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
    }

    private static Microsoft.SemanticKernel.Connectors.Google.VertexAIVersion GetVertexAIVersion(VertexAIVersion apiVersion)
    {
        return apiVersion switch
        {
            VertexAIVersion.V1 => Microsoft.SemanticKernel.Connectors.Google.VertexAIVersion.V1,
            _ => throw new ArgumentOutOfRangeException($"Invalid api version '{apiVersion}'.")
        };
    }
}
