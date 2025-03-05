using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel;

internal sealed class AzureOpenAIMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly AzureOpenAIOptions _options;
    private readonly GlobalAzureOpenAIOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public AzureOpenAIMemoryBuilderConfigurator(
        AzureOpenAIOptions options,
        GlobalAzureOpenAIOptions? globalOptions = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
        _globalOptions = globalOptions;
        _httpClientFactory = httpClientFactory;
    }

    public void Configure(MemoryBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        ConfigureAzureOpenAITextEmbeddingGeneration(builder);
    }

    private void ConfigureAzureOpenAITextEmbeddingGeneration(MemoryBuilder builder)
    {
        var options = _options.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.Endpoint ??= _globalOptions.Endpoint;
            options.ApiKey ??= _globalOptions.ApiKey;
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
