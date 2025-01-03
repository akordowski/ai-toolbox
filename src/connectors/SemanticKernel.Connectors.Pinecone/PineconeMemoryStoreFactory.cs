using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Pinecone;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class PineconeMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Pinecone;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(PineconeMemoryStoreOptions)}' provided.");

        return new PineconeMemoryStore(
            opt.PineconeEnvironment,
            opt.ApiKey);
    }
}
