using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.SqlServer;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class SqlServerMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.SqlServer;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(SqlServerMemoryStoreOptions)}' provided.");

        return new SqlServerMemoryStore(
            opt.ConnectionString,
            opt.Schema);
    }
}
