using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class AzureCosmosDBMongoDBMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly AzureCosmosDBMongoDBMemoryStoreOptions _options;

    public AzureCosmosDBMongoDBMemoryStoreFactory(AzureCosmosDBMongoDBMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null)
    {
        var config = new AzureCosmosDBMongoDBConfig(_options.Dimensions);

        return new AzureCosmosDBMongoDBMemoryStore(
            _options.ConnectionString,
            _options.DatabaseName,
            config);
    }
}
