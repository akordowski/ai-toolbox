using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Pinecone;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class PineconeMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly PineconeMemoryStoreOptions _options;

    public PineconeMemoryStoreFactory(PineconeMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new PineconeMemoryStore(
            _options.PineconeEnvironment,
            _options.ApiKey);
}
