using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Redis;
using Microsoft.SemanticKernel.Memory;
using NRedisStack.Search;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class RedisMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Redis;

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt, $"No '{nameof(RedisMemoryStoreOptions)}' provided.");

        return new RedisMemoryStore(
            opt.ConnectionString,
            opt.VectorSize,
            (Schema.VectorField.VectorAlgo)opt.VectorIndexAlgorithm,
            (VectorDistanceMetric)opt.VectorDistanceMetric,
            opt.QueryDialect);
    }
}
