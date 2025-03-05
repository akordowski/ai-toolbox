using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Milvus;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

internal sealed class MilvusMemoryStoreFactory : IMemoryStoreFactory
{
    private readonly MilvusMemoryStoreOptions _options;

    public MilvusMemoryStoreFactory(MilvusMemoryStoreOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _options = options;
    }

    public IMemoryStore GetMemoryStore(ILoggerFactory? loggerFactory = null, HttpClient? httpClient = null) =>
        new MilvusMemoryStore(
            _options.Host,
            _options.Port,
            _options.Ssl,
            _options.Database,
            _options.IndexName,
            _options.VectorSize,
            (Milvus.Client.SimilarityMetricType)_options.MetricType,
            (Milvus.Client.ConsistencyLevel)_options.ConsistencyLevel);
}
