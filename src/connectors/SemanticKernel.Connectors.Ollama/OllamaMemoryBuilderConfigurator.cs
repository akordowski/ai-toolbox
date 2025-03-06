using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;
using OllamaSharp;

namespace AIToolbox.SemanticKernel;

internal sealed class OllamaMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly OllamaOptions _options;
    private readonly GlobalOllamaOptions? _globalOptions;
    private readonly OllamaApiClient? _ollamaApiClient;

    public OllamaMemoryBuilderConfigurator(
        OllamaOptions options,
        GlobalOllamaOptions? globalOptions = null,
        OllamaApiClient? ollamaApiClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
        _globalOptions = globalOptions;
        _ollamaApiClient = ollamaApiClient;
    }

    public void Configure(MemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureTextEmbeddingGeneration(builder);
    }

    private void ConfigureTextEmbeddingGeneration(MemoryBuilder builder)
    {
        var client = _ollamaApiClient;

        if (client is null)
        {
            var options = _options.TextEmbeddingGeneration;

            if (options is null)
            {
                return;
            }

            if (_globalOptions is not null)
            {
                options.Endpoint ??= _globalOptions.Endpoint;
            }

            client = new OllamaApiClient(options.Endpoint!, options.ModelId);
        }

        builder.WithTextEmbeddingGeneration(client.AsTextEmbeddingGenerationService());
    }
}
