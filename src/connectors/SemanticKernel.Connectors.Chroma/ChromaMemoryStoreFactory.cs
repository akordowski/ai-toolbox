using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Chroma;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class ChromaMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Chroma;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(ChromaMemoryStoreOptions)}' provided.");

        return new ChromaMemoryStore(opt.Endpoint, loggerFactory);
    }
}
