using AIToolbox.Options.SemanticKernel;
using Kusto.Data.Common;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Kusto;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class KustoMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly ICslAdminProvider _cslAdminProvider;
    private readonly ICslQueryProvider _cslQueryProvider;

    public KustoMemoryStoreFactory(
        ICslAdminProvider cslAdminProvider,
        ICslQueryProvider cslQueryProvider)
    {
        _cslAdminProvider = cslAdminProvider;
        _cslQueryProvider = cslQueryProvider;
    }

    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Kusto;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(KustoMemoryStoreOptions)}' provided.");

        return new KustoMemoryStore(
            _cslAdminProvider,
            _cslQueryProvider,
            opt.Database);
    }
}
