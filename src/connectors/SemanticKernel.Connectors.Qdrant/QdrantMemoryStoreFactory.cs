using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class QdrantMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Qdrant;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(QdrantMemoryStoreOptions)}' provided.");

        return new QdrantMemoryStore(
            opt.Endpoint,
            opt.VectorSize,
            loggerFactory);
    }
}
