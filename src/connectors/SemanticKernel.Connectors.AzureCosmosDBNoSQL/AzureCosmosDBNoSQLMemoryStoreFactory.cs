using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class AzureCosmosDBNoSQLMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly AzureCosmosDBNoSQLMemoryStoreOptions _options;

    public AzureCosmosDBNoSQLMemoryStoreFactory(AzureCosmosDBNoSQLMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new AzureCosmosDBNoSQLMemoryStore(
            _options.ConnectionString,
            _options.DatabaseName,
            _options.Dimensions,
            (Microsoft.Azure.Cosmos.VectorDataType)_options.VectorDataType,
            (Microsoft.Azure.Cosmos.VectorIndexType)_options.VectorIndexType,
            _options.ApplicationName);
}
