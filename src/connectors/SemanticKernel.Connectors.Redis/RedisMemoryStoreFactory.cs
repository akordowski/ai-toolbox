using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Redis;
using Microsoft.SemanticKernel.Memory;
using NRedisStack.Search;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class RedisMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly RedisMemoryStoreOptions _options;

    public RedisMemoryStoreFactory(RedisMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new RedisMemoryStore(
            _options.ConnectionString,
            _options.VectorSize,
            (Schema.VectorField.VectorAlgo)_options.VectorIndexAlgorithm,
            (VectorDistanceMetric)_options.VectorDistanceMetric,
            _options.QueryDialect);
}
