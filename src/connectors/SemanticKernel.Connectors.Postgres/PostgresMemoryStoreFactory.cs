using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Postgres;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class PostgresMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Postgres;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(PostgresMemoryStoreOptions)}' provided.");

        return new PostgresMemoryStore(
            opt.ConnectionString,
            opt.VectorSize,
            opt.Schema);
    }
}
