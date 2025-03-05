using AIToolbox.Options.SemanticKernel;
using Kusto.Data.Common;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Kusto;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class KustoMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly KustoMemoryStoreOptions _options;
    private readonly ICslAdminProvider _cslAdminProvider;
    private readonly ICslQueryProvider _cslQueryProvider;

    public KustoMemoryStoreFactory(
        KustoMemoryStoreOptions options,
        ICslAdminProvider cslAdminProvider,
        ICslQueryProvider cslQueryProvider)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
        _cslAdminProvider = cslAdminProvider;
        _cslQueryProvider = cslQueryProvider;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new KustoMemoryStore(
            _cslAdminProvider,
            _cslQueryProvider,
            _options.Database);
}
