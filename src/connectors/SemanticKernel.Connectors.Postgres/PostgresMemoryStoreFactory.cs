using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Postgres;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class PostgresMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly PostgresMemoryStoreOptions _options;

    public PostgresMemoryStoreFactory(PostgresMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new PostgresMemoryStore(
            _options.ConnectionString,
            _options.VectorSize,
            _options.Schema);
}
