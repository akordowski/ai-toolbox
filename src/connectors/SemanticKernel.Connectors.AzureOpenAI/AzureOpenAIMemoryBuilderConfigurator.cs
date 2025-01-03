using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel;

public sealed class AzureOpenAIMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly MemoryOptions _memoryOptions;
    private readonly AzureOpenAIConnectorOptions? _connectorOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public AzureOpenAIMemoryBuilderConfigurator(
        MemoryOptions memoryOptions,
        AzureOpenAIConnectorOptions? connectorOptions = null,
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

        ConfigureAzureOpenAITextEmbeddingGeneration(builder);
    }

    private void ConfigureAzureOpenAITextEmbeddingGeneration(MemoryBuilder builder)
    {
        var options = _memoryOptions.TextEmbeddingGeneration?.AzureOpenAI;

        if (options is null)
        {
            return;
        }

        if (_connectorOptions is not null)
        {
            options.Endpoint ??= _connectorOptions.Endpoint;
            options.ApiKey ??= _connectorOptions.ApiKey;
        }

        // TODO: Check for extension method
        // See: https://github.com/microsoft/semantic-kernel/issues/8658

        //builder.WithAzureOpenAITextEmbeddingGeneration(
        //    options.DeploymentName,
        //    options.Endpoint!,
        //    options.ApiKey!,
        //    options.ModelId,
        //    _httpClientFactory?.CreateClient(),
        //    options.Dimensions);

        // TODO: Delete this implementation if extension methods are available
        builder.WithTextEmbeddingGeneration((loggerFactory, _) =>
            new AzureOpenAITextEmbeddingGenerationService(
                options.DeploymentName,
                options.Endpoint!,
                options.ApiKey!,
                options.ModelId,
                _httpClientFactory?.CreateClient(),
                loggerFactory,
                options.Dimensions));
    }
}
