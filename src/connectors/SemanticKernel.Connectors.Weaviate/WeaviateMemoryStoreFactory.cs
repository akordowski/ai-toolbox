using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Weaviate;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class WeaviateMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Weaviate;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(WeaviateMemoryStoreOptions)}' provided.");

        return new WeaviateMemoryStore(
            opt.Endpoint,
            opt.ApiKey,
            opt.ApiVersion,
            loggerFactory);
    }
}
