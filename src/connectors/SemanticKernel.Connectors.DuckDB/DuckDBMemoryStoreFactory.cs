using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.DuckDB;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class DuckDBMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly DuckDBMemoryStoreOptions _options;

    public DuckDBMemoryStoreFactory(DuckDBMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        _options.VectorSize is null
            ? DuckDBMemoryStore.ConnectAsync(_options.Filename).GetAwaiter().GetResult()
            : DuckDBMemoryStore.ConnectAsync(_options.Filename, (int)_options.VectorSize).GetAwaiter().GetResult();
}
