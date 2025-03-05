using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.MongoDB;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class MongoDBMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly MongoDBMemoryStoreOptions _options;

    public MongoDBMemoryStoreFactory(MongoDBMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new MongoDBMemoryStore(
            _options.ConnectionString,
            _options.DatabaseName,
            _options.IndexName);
}
