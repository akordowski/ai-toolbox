using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class QdrantMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly QdrantMemoryStoreOptions _options;

    public QdrantMemoryStoreFactory(QdrantMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new QdrantMemoryStore(
            _options.Endpoint,
            _options.VectorSize,
            loggerFactory);
}
