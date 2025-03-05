using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel;

internal sealed class MemoryProvider : IMemoryProvider
{
    private readonly MemoryOptions _options;
    private readonly IMemoryStoreFactory? _memoryStoreFactory;
    private readonly ITextEmbeddingGenerationService? _textEmbeddingGeneration;
    private readonly ILoggerFactory? _loggerFactory;

    public MemoryProvider(
        MemoryOptions options,
        IMemoryStoreFactory? memoryStoreFactory = null,
        ITextEmbeddingGenerationService? textEmbeddingGeneration = null,
        ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
        _memoryStoreFactory = memoryStoreFactory;
        _textEmbeddingGeneration = textEmbeddingGeneration;
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    public ISemanticTextMemory GetMemory()
    {
        var builder = new MemoryBuilder();

        ConfigureLogging(builder);
        ConfigureMemoryStore(builder);
        ConfigureTextEmbeddingGeneration(builder);

        return builder.Build();
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
