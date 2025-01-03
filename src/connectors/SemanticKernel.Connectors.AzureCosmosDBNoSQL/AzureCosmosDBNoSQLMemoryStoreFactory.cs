using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class AzureCosmosDBNoSQLMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.AzureCosmosDBNoSQL;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(AzureCosmosDBNoSQLMemoryStoreOptions)}' provided.");

        return new AzureCosmosDBNoSQLMemoryStore(
            opt.ConnectionString,
            opt.DatabaseName,
            opt.Dimensions,
            (Microsoft.Azure.Cosmos.VectorDataType)opt.VectorDataType,
            (Microsoft.Azure.Cosmos.VectorIndexType)opt.VectorIndexType,
            opt.ApplicationName);
    }
}
