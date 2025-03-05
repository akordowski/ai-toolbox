using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.SqlServer;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class SqlServerMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly SqlServerMemoryStoreOptions _options;

    public SqlServerMemoryStoreFactory(SqlServerMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new SqlServerMemoryStore(
            _options.ConnectionString,
            _options.Schema);
}
