using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;
using OllamaSharp;

namespace AIToolbox.SemanticKernel;

public sealed class OllamaMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly MemoryOptions _memoryOptions;
    private readonly OllamaConnectorOptions? _ollamaConnectorOptions;
    private readonly OllamaApiClient? _ollamaApiClient;
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly ILoggerFactory? _loggerFactory;

    public OllamaMemoryBuilderConfigurator(
        MemoryOptions memoryOptions,
        OllamaConnectorOptions? ollamaConnectorOptions = null,
        OllamaApiClient? ollamaApiClient = null,
        IHttpClientFactory? httpClientFactory = null,
        ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(memoryOptions, nameof(memoryOptions));

        _memoryOptions = memoryOptions;
        _ollamaConnectorOptions = ollamaConnectorOptions;
        _ollamaApiClient = ollamaApiClient;
        _httpClientFactory = httpClientFactory;
        _loggerFactory = loggerFactory;
    }

    public void Configure(MemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureTextEmbeddingGeneration(builder);
    }

    private void ConfigureTextEmbeddingGeneration(MemoryBuilder builder)
    {
        var options = _memoryOptions.TextEmbeddingGeneration!.Ollama;

        if (options is null)
        {
            return;
        }

        if (_ollamaConnectorOptions is not null)
        {
            options.Endpoint ??= _ollamaConnectorOptions.Endpoint;
        }

        ITextEmbeddingGenerationService? service;

        if (_ollamaApiClient is not null)
        {
            service = new OllamaTextEmbeddingGenerationService(
                modelId: options.ModelId,
                ollamaClient: _ollamaApiClient,
                loggerFactory: _loggerFactory);
        }
        else if (_httpClientFactory is not null)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            service = new OllamaTextEmbeddingGenerationService(
                modelId: options.ModelId,
                httpClient: _httpClientFactory.CreateClient(),
                loggerFactory: _loggerFactory);
#pragma warning restore CA2000 // Dispose objects before losing scope
        }
        else
        {
            service = new OllamaTextEmbeddingGenerationService(
                modelId: options.ModelId,
                endpoint: new Uri(options.Endpoint!),
                loggerFactory: _loggerFactory);
        }

        // TODO: Refactor when the connector provides proper extension methods
        builder.WithTextEmbeddingGeneration(service);
    }
}
