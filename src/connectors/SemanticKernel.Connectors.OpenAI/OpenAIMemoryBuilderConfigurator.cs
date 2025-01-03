using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel;

public sealed class OpenAIMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly MemoryOptions _memoryOptions;
    private readonly OpenAIConnectorOptions? _connectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public OpenAIMemoryBuilderConfigurator(
        MemoryOptions memoryOptions,
        OpenAIConnectorOptions? connectorOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(memoryOptions, nameof(memoryOptions));

        _memoryOptions = memoryOptions;
        _connectorOptions = connectorOptions;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(MemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureOpenAITextEmbeddingGeneration(builder);
    }

    private void ConfigureOpenAITextEmbeddingGeneration(MemoryBuilder builder)
    {
        var options = _memoryOptions.TextEmbeddingGeneration?.OpenAI;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.ApiKey ??= _connectorOptions.ApiKey;
            options.OrgId ??= _connectorOptions.OrgId;
        }

#pragma warning disable CA2000 // Dispose objects before losing scope
        builder.WithOpenAITextEmbeddingGeneration(
            options.ModelId,
            options.ApiKey!,
            options.OrgId,
            _httpClientFactory?.CreateClient(),
            options.Dimensions);
#pragma warning restore CA2000 // Dispose objects before losing scope
    }
}
