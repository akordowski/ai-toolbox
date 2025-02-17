using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Connectors.Milvus;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public sealed class MilvusMemoryStoreFactory : IMemoryStoreFactory
{
    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.Milvus!;

        Verify.ThrowIfOptionsNull(opt);

        return new MilvusMemoryStore(
            opt.Host,
            opt.Port,
            opt.Ssl,
            opt.Database,
            opt.IndexName,
            opt.VectorSize,
            (Milvus.Client.SimilarityMetricType)opt.MetricType,
            (Milvus.Client.ConsistencyLevel)opt.ConsistencyLevel);
    }
}
