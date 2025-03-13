using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel;

internal sealed class OpenAIMemoryBuilderConfigurator : IMemoryBuilderConfigurator
{
    private readonly OpenAIOptions _options;
    private readonly GlobalOpenAIOptions? _globalOptions;
    private readonly IHttpClientFactory? _httpClientFactory;

    public OpenAIMemoryBuilderConfigurator(
        OpenAIOptions options,
        GlobalOpenAIOptions? globalOptions = null,
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

        ConfigureTextEmbeddingGeneration(builder);
    }

    private void ConfigureTextEmbeddingGeneration(MemoryBuilder builder)
    {
        var options = _options.TextEmbeddingGeneration;

        if (options is null)
        {
            return;
        }

        if (_globalOptions is not null)
        {
            options.ApiKey ??= _globalOptions.ApiKey;
            options.OrgId ??= _globalOptions.OrgId;
        }

        builder.WithOpenAITextEmbeddingGeneration(
            options.ModelId,
            options.ApiKey!,
            options.OrgId,
            _httpClientFactory?.CreateClient(),
            options.Dimensions);
    }
}
