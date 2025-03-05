using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Sqlite;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class SqliteMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly SqliteMemoryStoreOptions _options;

    public SqliteMemoryStoreFactory(SqliteMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        SqliteMemoryStore.ConnectAsync(_options.Filename).GetAwaiter().GetResult();
}
