using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class AzureCosmosDBMongoDBMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.AzureCosmosDBMongoDB;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(AzureCosmosDBMongoDBMemoryStoreOptions)}' provided.");

        var config = new AzureCosmosDBMongoDBConfig(opt.Dimensions);

        return new AzureCosmosDBMongoDBMemoryStore(
            opt.ConnectionString,
            opt.DatabaseName,
            config);
    }
}
