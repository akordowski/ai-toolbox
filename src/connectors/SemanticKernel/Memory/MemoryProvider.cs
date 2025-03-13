using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class MemoryProvider : IMemoryProvider
{
    private readonly MemoryOptions _options;
    private readonly IEnumerable<IMemoryBuilderConfigurator> _configurators;
    private readonly IMemoryStoreFactory? _memoryStoreFactory;
    private readonly ITextEmbeddingGenerationService? _textEmbeddingGeneration;
    private readonly ILoggerFactory? _loggerFactory;

    public MemoryProvider(
        MemoryOptions options,
        IEnumerable<IMemoryBuilderConfigurator> configurators,
        IMemoryStoreFactory? memoryStoreFactory = null,
        ITextEmbeddingGenerationService? textEmbeddingGeneration = null,
        ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(configurators, nameof(configurators));

        _options = options;
        _configurators = configurators;
        _memoryStoreFactory = memoryStoreFactory;
        _textEmbeddingGeneration = textEmbeddingGeneration;
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    public ISemanticTextMemory GetMemory()
    {
        var builder = new MemoryBuilder();

        InvokeConfigurators(builder);
        ConfigureLogging(builder);
        ConfigureMemoryStore(builder);
        ConfigureTextEmbeddingGeneration(builder);

        return builder.Build();
    }

    private void InvokeConfigurators(MemoryBuilder builder)
    {
        foreach (var configurator in _configurators)
        {
            configurator.Configure(builder);
        }
    }

    private void ConfigureLogging(MemoryBuilder builder)
    {
        if (_loggerFactory is not null)
        {
            builder.WithLoggerFactory(_loggerFactory);
        }
    }

    private void ConfigureMemoryStore(MemoryBuilder builder)
    {
        if (_memoryStoreFactory is not null)
        {
            builder.WithMemoryStore(_memoryStoreFactory.GetMemoryStore);
        }
    }

    private void ConfigureTextEmbeddingGeneration(MemoryBuilder builder)
    {
        if (_textEmbeddingGeneration is not null)
        {
            builder.WithTextEmbeddingGeneration(_textEmbeddingGeneration);
        }
    }
}
