using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
using GoogleAIVersion = AIToolbox.Options.GoogleAIVersion;
using VertexAIVersion = AIToolbox.Options.VertexAIVersion;

namespace AIToolbox.SemanticKernel;

public sealed class GoogleKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly KernelOptions _kernelOptions;
    private readonly GlobalGoogleOptions? _googleConnectorOptions;
    private readonly GlobalVertexAIOptions? _vertexAIConnectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public GoogleKernelBuilderConfigurator(
        KernelOptions kernelOptions,
        GlobalGoogleOptions? googleConnectorOptions = null,
        GlobalVertexAIOptions? vertexAIConnectorOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(kernelOptions, nameof(kernelOptions));

        _kernelOptions = kernelOptions;
        _googleConnectorOptions = googleConnectorOptions;
        _vertexAIConnectorOptions = vertexAIConnectorOptions;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureGoogleAI(builder);
        ConfigureVertexAI(builder);
        ConfigurePromptExecutionSettingsMapper(builder);
    }

    private void ConfigureGoogleAI(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.GoogleAI;

        if (options is null)
        {
            return;
        }

        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureGoogleAIGeminiChatCompletion(builder);
        ConfigureGoogleAIEmbeddingGeneration(builder);
    }

    private void ConfigureVertexAI(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.VertexAI;

        if (options is null)
        {
            return;
        }

        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureVertexAIGeminiChatCompletion(builder);
        ConfigureVertexAIEmbeddingGeneration(builder);
    }

    private void ConfigureGoogleAIGeminiChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.GoogleAI?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_googleConnectorOptions is not null)
        {
            options.ApiKey ??= _googleConnectorOptions.ApiKey;
            options.ApiVersion ??= _googleConnectorOptions.ApiVersion;
            options.ServiceId ??= _googleConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddGoogleAIGeminiChatCompletion(
            modelId: options.ModelId,
            apiKey: options.ApiKey!,
            apiVersion: GetGoogleAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureGoogleAIEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.GoogleAI?.EmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_googleConnectorOptions is not null)
        {
            options.ApiKey ??= _googleConnectorOptions.ApiKey;
            options.ApiVersion ??= _googleConnectorOptions.ApiVersion;
            options.ServiceId ??= _googleConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddGoogleAIEmbeddingGeneration(modelId: options.ModelId,
            apiKey: options.ApiKey!,
            apiVersion: GetGoogleAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureVertexAIGeminiChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.VertexAI?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_vertexAIConnectorOptions is not null)
        {
            options.BearerKey ??= _vertexAIConnectorOptions.BearerKey;
            options.Location ??= _vertexAIConnectorOptions.Location;
            options.ProjectId ??= _vertexAIConnectorOptions.ProjectId;
            options.ApiVersion ??= _vertexAIConnectorOptions.ApiVersion;
            options.ServiceId ??= _vertexAIConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddVertexAIGeminiChatCompletion(
            modelId: options.ModelId,
            bearerKey: options.BearerKey!,
            location: options.Location!,
            projectId: options.ProjectId!,
            apiVersion: GetVertexAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private void ConfigureVertexAIEmbeddingGeneration(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.VertexAI?.EmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_vertexAIConnectorOptions is not null)
        {
            options.BearerKey ??= _vertexAIConnectorOptions.BearerKey;
            options.Location ??= _vertexAIConnectorOptions.Location;
            options.ProjectId ??= _vertexAIConnectorOptions.ProjectId;
            options.ApiVersion ??= _vertexAIConnectorOptions.ApiVersion;
            options.ServiceId ??= _vertexAIConnectorOptions.ServiceId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.AddVertexAIEmbeddingGeneration(
            modelId: options.ModelId,
            bearerKey: options.BearerKey!,
            location: options.Location!,
            projectId: options.ProjectId!,
            apiVersion: GetVertexAIVersion(options.ApiVersion!.Value),
            serviceId: options.ServiceId,
            httpClient: _httpClientFactory?.CreateClient());
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    private static void ConfigurePromptExecutionSettingsMapper(IKernelBuilder builder)
    {
        // GeminiPromptExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, GeminiPromptExecutionSettingsMapper>(typeof(GoogleAIGeminiChatCompletionService));
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, GeminiPromptExecutionSettingsMapper>(typeof(VertexAIGeminiChatCompletionService));
    }

    private static Microsoft.SemanticKernel.Connectors.Google.GoogleAIVersion GetGoogleAIVersion(GoogleAIVersion apiVersion)
    {
        return apiVersion switch
        {
            GoogleAIVersion.V1 => Microsoft.SemanticKernel.Connectors.Google.GoogleAIVersion.V1,
            GoogleAIVersion.V1Beta => Microsoft.SemanticKernel.Connectors.Google.GoogleAIVersion.V1_Beta,
            _ => throw new ArgumentOutOfRangeException($"Invalid api varsion '{apiVersion}'.")
        };
    }

    private static Microsoft.SemanticKernel.Connectors.Google.VertexAIVersion GetVertexAIVersion(VertexAIVersion apiVersion)
    {
        return apiVersion switch
        {
            VertexAIVersion.V1 => Microsoft.SemanticKernel.Connectors.Google.VertexAIVersion.V1,
            _ => throw new ArgumentOutOfRangeException($"Invalid api varsion '{apiVersion}'.")
        };
    }
}
