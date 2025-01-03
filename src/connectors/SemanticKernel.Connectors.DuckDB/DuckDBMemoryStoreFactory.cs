using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.DuckDB;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class DuckDBMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.DuckDB;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(DuckDBMemoryStoreOptions)}' provided.");

        return opt.VectorSize is null
            ? DuckDBMemoryStore.ConnectAsync(opt.Filename).GetAwaiter().GetResult()
            : DuckDBMemoryStore.ConnectAsync(opt.Filename, (int)opt.VectorSize).GetAwaiter().GetResult();
    }
}
