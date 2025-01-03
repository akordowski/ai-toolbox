using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Sqlite;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class SqliteMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Sqlite;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(SqliteMemoryStoreOptions)}' provided.");

        return SqliteMemoryStore.ConnectAsync(opt.Filename).GetAwaiter().GetResult();
    }
}
