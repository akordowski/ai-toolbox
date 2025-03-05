using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Chroma;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class ChromaMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly ChromaMemoryStoreOptions _options;

    public ChromaMemoryStoreFactory(ChromaMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new ChromaMemoryStore(_options.Endpoint, loggerFactory);
}
