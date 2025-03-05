using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Weaviate;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class WeaviateMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly WeaviateMemoryStoreOptions _options;

    public WeaviateMemoryStoreFactory(WeaviateMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new WeaviateMemoryStore(
            _options.Endpoint,
            _options.ApiKey,
            _options.ApiVersion,
            loggerFactory);
}
