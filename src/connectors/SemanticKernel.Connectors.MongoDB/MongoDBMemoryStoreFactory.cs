using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.MongoDB;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class MongoDBMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.MongoDB;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(MongoDBMemoryStoreOptions)}' provided.");

        return new MongoDBMemoryStore(
            opt.ConnectionString,
            opt.DatabaseName,
            opt.IndexName);
    }
}
