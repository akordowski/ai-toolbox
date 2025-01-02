using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;
using OllamaSharp;

namespace AIToolbox.SemanticKernel;

public sealed class OllamaKernelBuilderConfigurator : IKernelBuilderConfigurator
{
    private readonly KernelOptions _kernelOptions;
    private readonly OllamaConnectorOptions? _ollamaConnectorOptions;
    private readonly OllamaApiClient? _ollamaApiClient;
    private readonly IHttpClientFactory? _httpClientFactory;

    public OllamaKernelBuilderConfigurator(
        KernelOptions kernelOptions,
        OllamaConnectorOptions? ollamaConnectorOptions = null,
        OllamaApiClient? ollamaApiClient = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(kernelOptions, nameof(kernelOptions));

        _kernelOptions = kernelOptions;
        _ollamaConnectorOptions = ollamaConnectorOptions;
        _ollamaApiClient = ollamaApiClient;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureChatCompletion(builder);
        ConfigureTextEmbeddingGeneration(builder);
        ConfigureTextGeneration(builder);
        ConfigurePromptExecutionSettingsMapper(builder);
    }

    private void ConfigureChatCompletion(IKernelBuilder builder)
    {
        var options = _kernelOptions.Connectors?.Ollama?.ChatCompletion;

        if (options is null)
        {
            return;
        }

        if (_ollamaConnectorOptions is not null)
        {
            options.Endpoint ??= _ollamaConnectorOptions.Endpoint;
            options.ServiceId ??= _ollamaConnectorOptions.ServiceId;
        }

        if (_ollamaApiClient is not null)
        {
            builder.AddOllamaChatCompletion(
                modelId: options.ModelId,
                ollamaClient: _ollamaApiClient,
                serviceId: options.ServiceId);
        }
        else if (_httpClientFactory is not null)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            builder.AddOllamaChatCompletion(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                serviceId: options.ServiceId);
#pragma warning restore CA2000 // Dispose objects before losing scope
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
        var options = _kernelOptions.Connectors?.Ollama?.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_ollamaConnectorOptions is not null)
        {
            options.Endpoint ??= _ollamaConnectorOptions.Endpoint;
            options.ServiceId ??= _ollamaConnectorOptions.ServiceId;
        }

        if (_ollamaApiClient is not null)
        {
            builder.AddOllamaTextEmbeddingGeneration(
                modelId: options.ModelId,
                ollamaClient: _ollamaApiClient,
                serviceId: options.ServiceId);
        }
        else if (_httpClientFactory is not null)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            builder.AddOllamaTextEmbeddingGeneration(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                serviceId: options.ServiceId);
#pragma warning restore CA2000 // Dispose objects before losing scope
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
        var options = _kernelOptions.Connectors?.Ollama?.TextGeneration;

        if (options is null)
        {
            return;
        }

        if (_ollamaConnectorOptions is not null)
        {
            options.Endpoint ??= _ollamaConnectorOptions.Endpoint;
            options.ServiceId ??= _ollamaConnectorOptions.ServiceId;
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
#pragma warning disable CA2000 // Dispose objects before losing scope
            builder.AddOllamaTextGeneration(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                serviceId: options.ServiceId);
#pragma warning restore CA2000 // Dispose objects before losing scope
        }
        else
        {
            builder.AddOllamaTextGeneration(
                modelId: options.ModelId,
                endpoint: new Uri(options.Endpoint!),
                serviceId: options.ServiceId);
        }
    }

    private static void ConfigurePromptExecutionSettingsMapper(IKernelBuilder builder)
    {
        // OllamaPromptExecutionSettings
        builder.Services.AddKeyedSingleton<IPromptExecutionSettingsMapper, OllamaPromptExecutionSettingsMapper>(typeof(OllamaChatCompletionService));
    }
}
