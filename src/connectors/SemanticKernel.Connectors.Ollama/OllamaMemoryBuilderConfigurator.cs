using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;
using OllamaSharp;

namespace AIToolbox.SemanticKernel;

internal sealed class OllamaMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly OllamaApiClient _ollamaApiClient;

    public OllamaMemoryBuilderConfigurator(OllamaApiClient ollamaApiClient)
    {
        ArgumentNullException.ThrowIfNull(ollamaApiClient, nameof(ollamaApiClient));

        _ollamaApiClient = ollamaApiClient;
    }

    public void Configure(MemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureTextEmbeddingGeneration(builder);
    }

    private void ConfigureTextEmbeddingGeneration(MemoryBuilder builder)
    {
        builder.WithTextEmbeddingGeneration(_ollamaApiClient.AsTextEmbeddingGenerationService());
    }
}
